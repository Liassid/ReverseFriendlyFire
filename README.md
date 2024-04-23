# ReverseFriendlyFire
Basic implementation of a mechanic "borrowed" from R6S designed for limiting teamkillers' impact on regular players. Once a player kills or deals enough damage to teammates, all further damage is reversed to teamkiller themselves. Even if they leave, teamkills will still be recorded and Damage Reversing state will be saved

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
  # What death message should be shown to player when dying due to Damage Reversing?
  reverse_damage_reason: 'Teamkilling'
  # What hint should be shown to player when Damage Reversing is activated?
  reverse_damage_activation_hint:
  # The hint content
    content: '<color=red>Reverse Friendly Fire activated</color>\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n'
    # The hint duration
    duration: 5
    # Indicates whether the hint should be shown or not
    show: true
```

### API
Currently, plugin only offers Damage Reversing activation event (`ReverseFriendlyFire.Events.Events.ReverseDamageActivated`) so you can e.g. integrate it with your logging solution
