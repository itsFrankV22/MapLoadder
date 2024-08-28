using System;
using System.Threading;
using System.Threading.Tasks; // Necesario para usar Task.Delay
using Microsoft.Xna.Framework;  // Usar esta directiva para colores
using TShockAPI;
using TerrariaApi.Server;
using Terraria;
using Terraria.ID;

namespace MapLoad
{
    [ApiVersion(2, 1)]
    public class MapPlugin : TerrariaPlugin
    {
        public override string Author => "FrankV22";
        public override string Description => "LoadMap";
        public override string Name => "MapLoadder";
        public override Version Version => new Version(1, 0, 0, 0);

        private static CancellationTokenSource _cancellationTokenSource;

        public MapPlugin(Main game) : base(game)
        {
            // Constructor que acepta un parámetro Main y lo pasa al constructor base.
        }

        public override void Initialize()
        {
            Commands.ChatCommands.Add(new Command("map.load", MapLoad, "map"));
        }

        private void MapLoad(CommandArgs args)
        {
            if (args.Parameters.Count > 0 && args.Parameters[0].ToLower() == "load")
            {
                if (args.Player == null)
                {
                    args.Player.SendMessage("No se pudo acceder al jugador.", Color.Red);
                    return;
                }

                // Cancelar cualquier proceso en curso
                _cancellationTokenSource?.Cancel();
                _cancellationTokenSource = new CancellationTokenSource();

                // Iniciar el desbloqueo del mapa
                UnlockMapAsync(args.Player, _cancellationTokenSource.Token);
                args.Player.SendMessage("Desbloqueo del mapa en progreso. Por favor, espere.", Color.Yellow);
            }
            else
            {
                args.Player.SendMessage("Uso: /map load", Color.Red);  // Usar Microsoft.Xna.Framework.Color
            }
        }

        private async void UnlockMapAsync(TSPlayer player, CancellationToken token)
        {
            var p = player.TPlayer;
            var width = Main.maxTilesX;
            var height = Main.maxTilesY;
            int tileSize = 5; // Tamaño del área que se recorrerá en cada teletransportación
            int sectionWidth = 7; // Anchura para comenzar una nueva columna

            try
            {
                for (int x = 0; x < width; x += sectionWidth)
                {
                    for (int y = 0; y < height; y += tileSize)
                    {
                        // Verifica si se ha cancelado el proceso
                        token.ThrowIfCancellationRequested();

                        // Calcula la posición de teletransporte
                        int tx = Math.Min(x + tileSize / 2, width - 1);
                        int ty = Math.Min(y + tileSize / 2, height - 1);

                        // Teletransportar al jugador
                        player.Teleport(tx * 16, ty * 16, 0);

                        // Esperar 300 ms antes de continuar
                        await Task.Delay(300, token);
                    }

                    // Mover el área de exploración para la siguiente columna
                    p.position.X = x * 16; // Asegurarse de que no se pase de 7 de ancho
                }

                // Teletransportar al punto de spawn
                player.Teleport(Main.spawnTileX * 16, Main.spawnTileY * 16, 0);
                player.SendMessage("Mapa desbloqueado al 100%.", Color.Green);
            }
            catch (OperationCanceledException)
            {
                // Manejar la cancelación del proceso
                player.SendMessage("El proceso de escaneo ha sido cancelado.", Color.Red);
            }
        }
    }
}
