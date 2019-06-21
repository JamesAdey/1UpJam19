using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    public enum Mode
    {
        GAME,
        BUILD,
        END
    }

    [SerializeField]
    public GameObject healthBar;

    public HealthBar health;

    public Mode myMode = Mode.GAME;

    public BaseBrain brain;
    public PlayerInput inputs;
    public GameObject bulletPrefab;

    public float timeSinceShoot = 0;
    public float timeForShoot = 0.5f;

    float speed = 5;

    [SerializeField]
    public Teams.Team team;

    Transform thisTransform;
    Rigidbody thisRigid;

    TeamMatChanger[] matChangers;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<Transform>();
        brain = GetComponent<BaseBrain>();
        thisRigid = GetComponent<Rigidbody>();
        health = healthBar.GetComponent<HealthBar>();
        health.parentTransform = thisTransform;
        health.maxHealth = 300;
        UpdateVisuals();

    }

    private void UpdateVisuals()
    {
        if (matChangers == null)
        {
            matChangers = GetComponentsInChildren<TeamMatChanger>();
        }
        foreach (TeamMatChanger script in matChangers)
        {
            script.ChangeTeam(team);
        }
    }

    void Shoot()
    {
        Vector3 directionOfShoot = thisTransform.forward;
        GameObject bullet = Instantiate(bulletPrefab, transform.position + (directionOfShoot), Quaternion.identity);
        Vector3 targetPoint = new Vector3(inputs.lookPos.x, thisTransform.position.y, inputs.lookPos.z);
        var script = bullet.GetComponent<BulletScript>();
        script.targetPoint = targetPoint;
        script.team = GetTeam();
    }

    private void FixedUpdate()
    {
        switch (myMode)
        {
            case Mode.GAME:
                GameUpdate();
                break;
            case Mode.BUILD:
                BuildUpdate();
                break;
        }
    }

    private void BuildUpdate()
    {

    }

    private void GameUpdate()
    {
        inputs = brain.GetInputs();
        timeSinceShoot += Time.deltaTime;
        Vector3 moveVel = (inputs.strafeDir * inputs.leftRightInput) + (inputs.walkDir * inputs.forwardBackwardInput);
        moveVel.Normalize();
        moveVel *= speed;
        moveVel.y = thisRigid.velocity.y;
        thisRigid.velocity = moveVel;

        Vector3 flattenedLookPos = inputs.lookPos;
        flattenedLookPos.y = thisTransform.position.y;
        thisTransform.LookAt(flattenedLookPos);

        if (inputs.primaryAttack && timeSinceShoot > timeForShoot)
        {
            timeSinceShoot = 0;
            Shoot();
        }

        if (inputs.buildMode)
        {
            if (team == Teams.Team.PLAYER)
            {
                SpawnButtonController.spawner.OpenBuild();
                inputs.buildMode = false;
            }
            else if (team == Teams.Team.AI)
            {
                SpawnButtonController.spawner.PlayerEnterMode(Mode.BUILD, Teams.Team.AI);
                AIBuild(inputs.desiredBuilding,inputs.lookPos,inputs.buildingEuler);
                inputs.buildMode = false;
                SpawnButtonController.spawner.PlayerEnterMode(Mode.GAME, Teams.Team.AI);
                PlayerData myPlayer = GameManager.manager.GetPlayer(Teams.Team.AI);
                if (inputs.desiredBuilding == BuildingType.BARRACKS)
                {
                    myPlayer.resources -= 50;
                }
                if(inputs.desiredBuilding == BuildingType.TOWER)
                    {
                    myPlayer.resources -= 25;
                }
            }

        }
    }

    public void AIBuild(BuildingType type, Vector3 pos, Vector3 angles)
    {
        if (type == BuildingType.BARRACKS || type == BuildingType.TOWER)
        {
            SpawnButtonController.spawner.SpawnBuilding(type, team, pos, angles);
        }
    }

    public void TakeDamage(DamageInfo info)
    {
        health.inflictDamange(info.damage);

        if (health.isDead())
        {
            GameManager.manager.EndGame(GameManager.GetOpposingTeam(team));
        }
    }

    public Teams.Team GetTeam()
    {
        return team;
    }
}
