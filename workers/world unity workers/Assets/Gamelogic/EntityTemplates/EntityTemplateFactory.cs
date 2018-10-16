using Assets.Gamelogic.Core;
using Improbable.Character;
using Improbable.Core;
using Improbable.Player;
using Improbable.Unity.Core.Acls;
using Improbable.Unity.Entity;
using Improbable.Words;
using Improbable.Worker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EntityTemplateFactory {

    public static Entity CreatePlayerCreatingInstanceTemplate()
    {
        Entity playerCreatingInstanceEntityTemplate = EntityBuilder.Begin()
            .AddPositionComponent(new Vector3(0, 0, 0), CommonRequirementSets.PhysicsOnly)
            .AddMetadataComponent(SimulationSettings.PlayerCreatingInstancePrefabName)
            .SetPersistence(true)
            .SetReadAcl(CommonRequirementSets.PhysicsOrVisual)
            .AddComponent(new PlayerCreation.Data(), CommonRequirementSets.PhysicsOnly)
            .Build();

        return playerCreatingInstanceEntityTemplate;
    }

    public static Entity CreatePlayerCharacterTemplate(string clientWorkerId, Vector3 initialPosition)
    {
        CharacterControlsData ccd = new CharacterControlsData();
        ccd.characterControls = new Improbable.Collections.List<CharacterControlsUpdate>();

        CollisionsCreatedData colCreDat = new CollisionsCreatedData();
        colCreDat.newCollisions = new Improbable.Collections.List<NewCollision>();

        Entity playerCharacterEntityTemplate = EntityBuilder.Begin()
            .AddPositionComponent(initialPosition, CommonRequirementSets.PhysicsOnly)
            .AddMetadataComponent(SimulationSettings.PlayerCharacterPrefabName)
            .SetPersistence(false)
            .SetReadAcl(CommonRequirementSets.PhysicsOrVisual)
            .AddComponent(new ClientConnection.Data(SimulationSettings.TotalHeartbeatsBeforeTimeout), CommonRequirementSets.PhysicsOnly)
            .AddComponent(new CharacterControls.Data(ccd), CommonRequirementSets.SpecificClientOnly(clientWorkerId))
            .AddComponent(new ClientAuthorityCheck.Data(), CommonRequirementSets.SpecificClientOnly(clientWorkerId))
            .AddComponent(new PositionSetTimestamp.Data(0), CommonRequirementSets.PhysicsOnly)
            .AddComponent(new LiveTime.Data(0), CommonRequirementSets.PhysicsOnly)
            .AddComponent(new MessageSpawner.Data(), CommonRequirementSets.PhysicsOnly)
            .AddComponent(new CollisionsCreated.Data(colCreDat), CommonRequirementSets.PhysicsOnly)
            .Build();

        return playerCharacterEntityTemplate;
    }

    public static Entity CreateMessageOnGroundTemplate(Vector3 initialPosition, string message, bool isSolid)
    {
        Entity messageOnGroundTemplate;

        if (isSolid)
        {
            messageOnGroundTemplate = EntityBuilder.Begin()
            .AddPositionComponent(initialPosition, CommonRequirementSets.PhysicsOnly)
            .AddMetadataComponent(SimulationSettings.WalledMessageOnGroundPrefabName)
            .SetPersistence(true)
            .SetReadAcl(CommonRequirementSets.PhysicsOrVisual)
            .AddComponent(new Message.Data(message), CommonRequirementSets.PhysicsOnly)
            .Build();
        }
        else
        {
            messageOnGroundTemplate = EntityBuilder.Begin()
            .AddPositionComponent(initialPosition, CommonRequirementSets.PhysicsOnly)
            .AddMetadataComponent(SimulationSettings.MessageOnGroundPrefabName)
            .SetPersistence(true)
            .SetReadAcl(CommonRequirementSets.PhysicsOrVisual)
            .AddComponent(new Message.Data(message), CommonRequirementSets.PhysicsOnly)
            .Build();
        }

        return messageOnGroundTemplate;
    }

}
