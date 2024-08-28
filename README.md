# MapLoad Plugin for TShock
By FrankV22

MapLoad is a plugin for TShock that allows you to unlock the player's map and apply buffs during the process. Below is how to use the plugin and the required permissions.

Si hablas otro idioma por favor leer [README_SPANISH.md](https://github.com/itsFrankV22/MapLoadder/blob/main/README_SPANISH.md)

## Permissions and Commands

| Permission   | Command       | Description                                         |
|--------------|---------------|-----------------------------------------------------|
| `map.load`    | `/map load`   | Starts the map unlocking process. Applies buffs and teleports the player around the map. |

## Commands

### `/map load`

> [!WARNING]
> Before using "/map load", prepare by using "/godmode" and a mount that nullifies gravity so you don't fall while exploring.
> It is recommended to apply buffs like brightness and light pets for better visibility.

This command starts the map unlocking process for the player who executes it. When using this command:

- **Map Unlocking**: The player's map will be unlocked in a pattern that progresses from top to bottom. When a column is completed, a new column starts 40 blocks to the right.

- **Teleportation**: The player will be teleported to different locations on the map to unlock it progressively.
- **Cancellation Handling**: The process can be canceled at any time.

## Installation

1. **Requirements**: Ensure TShock is installed on your Terraria server.
2. **Plugin Installation**:
   - Copy the `MapLoad.dll` file to the `plugins` folder in your TShock installation on your server.
   - Restart the server to load the plugin.

Contributions are welcome. If you encounter any issues or want to improve the plugin, please open an **issue** or submit a **pull request**.

## Contact

For additional support or questions, contact:

- **Author**: FrankV22

---

Thank you for using MapLoad. Enjoy unlocking your map and applying buffs!
