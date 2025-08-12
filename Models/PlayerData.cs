using ProjectM.Network;
using Unity.Collections;
using Unity.Entities;

namespace ArenaBuilds.Models;

internal class PlayerData(User user = default, Entity userEntity = default)
{
    public User User { get; set; } = user;

    public Entity UserEntity { get; set; } = userEntity;

    public FixedString64Bytes CharacterName => User.CharacterName;

    public Entity CharacterEntity => User.LocalCharacter._Entity;
}