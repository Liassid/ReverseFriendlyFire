# ReverseFriendlyFire
Basic implementation of a mechanic "borrowed" from R6S designed for limiting teamkillers' impact on regular players. Once a player kills or deals enough damage to teammates, all further damage is reversed to teamkiller themselves. Even if they leave, teamkills will still be recorded and Damage Reversing state will be saved

![img](https://img.shields.io/github/downloads/Liassid/ReverseFriendlyFire/total)

### Commands
This plugin has only two commands at the moment: `rff toggle` for enabling and disabling RFF temporarily (e.g. for an event) and `rff info <players>` (`rff toggle @liassid.@hamr.` or `rff toggle 2.3.`) for checking specified players' Damage Reversing state, amount of teamkills and damage done to teammates

### Permissions
| Permission                     | Purpose                                |
|--------------------------------|----------------------------------------|
| `FriendlyFireDetectorImmunity` | Immunity to RFF (if enabled in config) |
| `rff.toggle`                   | Access to `rff toggle` command         |
| `rff.info`                     | Access to `rff info` command           |

### Default config
```yaml
reverse_friendly_fire:
  is_enabled: true
  debug: false
  # Should RFF be enabled at the start of the round?
  default_state: true
  # Should players with FriendlyFireDetectorImmunity permission be ignored?
  enable_immunity_permission: false
  # What amount of teamkills should activate Damage Reversing?
  kills_threshold: 1
  # What amount of damage to teammates should activate Damage Reversing?
  damage_threshold: 500
  # What multipliers should be applied on kills and damages with specified Damage Types? E.g. 0.34 kill multiplier will result in Damage Reversing activating only on 3rd kill with SCP-018
  damage_type_multipliers:
    Scp018:
      kills: 0.34
      damage: 0.34
```

### Default translations
```yaml
reverse_friendly_fire:
  reverse_damage_reason: 'You''ve been killed for teamkilling'
  reverse_damage_activation_hint:
  # The hint content
    content: '<color=red>Reverse Friendly Fire activated</color>\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n'
    # The hint duration
    duration: 5
    # Indicates whether the hint should be shown or not
    show: true
  parent_command_description: 'Reverse Friendly Fire management'
  parent_command_aliases: []
  toggle_command_description: 'enabling/disabling RFF'
  toggle_command_aliases:
  - 'tg'
  toggle_command_response: 'RFF has been %state%'
  toggle_command_rff_enabled: 'enabled'
  toggle_command_rff_disabled: 'disabled'
  info_command_description: 'checking players'' kills and damage'
  info_command_aliases:
  - 'i'
  info_command_usage: 'Usage: rff info <players>'
  info_command_table_header: 'Nickname | State | Kills | Damage'
  info_command_table_reverse_damage_enabled: 'Enabled'
  info_command_table_reverse_damage_disabled: 'Disabled'
  info_command_table_not_found: 'Not found'
  available_commands: 'Available commands:'
  insufficient_permissions: 'Insufficient permissions'
```

### API
Currently, plugin only offers Damage Reversing activation event (`ReverseFriendlyFire.Events.Events.ReverseDamageActivated`) so you can e.g. integrate it with your logging solution
