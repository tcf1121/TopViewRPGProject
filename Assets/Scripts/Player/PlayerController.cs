using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Player _player;

    public Vector2 InputDirection { get; private set; }
    public Vector3 LookDirection { get; private set; }

    private bool _delay;

    private void Awake() => Init();

    private void Init()
    {
        _player = GetComponent<Player>();
    }
    private void Update()
    {
        SetMovement();
    }

    private void SetMovement()
    {
        if (!_delay)
        {
            SetMove();
            SetRotate();
        }

    }

    private void SetMove()
    {
        //_player.Rigid.velocity = new Vector3(-InputDirection.y * _player.MoveSpeed, _player.Rigid.velocity.y, InputDirection.x * _player.MoveSpeed);
        Vector3 direction = new Vector3(-InputDirection.y, 0, InputDirection.x);
        transform.position += _player.playerStatus.MoveSpeed * Time.deltaTime * direction;
    }
    private void SetRotate()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);

        float rayLength;

        if (GroupPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointTolook = cameraRay.GetPoint(rayLength);
            LookDirection = new Vector3(pointTolook.x, transform.position.y, pointTolook.z);
            transform.LookAt(LookDirection);
        }
    }

    private void OnMove(InputValue value)
    {
        InputDirection = value.Get<Vector2>().normalized;
        if (InputDirection == Vector2.zero) IsMove(false);
        else IsMove(true);
    }


    private void OnJump(InputValue value)
    {
        if (!_player.playerStatus.IsJump && !_delay)
        {
            _player.Rigid.AddForce(Vector3.up * _player.playerStatus.JumpPower, ForceMode.Impulse);
            UseJump();
        }
    }

    private void OnInven(InputValue value)
    {
        Debug.Log("인벤 열기");
        if (!_player.inventory.getUIOn())
        {
            _player.playerStatus.IsOnUI = true;
            _player.inventory.OpenInventory(true);
        }
        else
        {
            _player.inventory.OpenInventory(false);
            _player.playerStatus.IsOnUI = AllUIOff();
        }
    }

    private bool AllUIOff()
    {
        if (_player.inventory.getUIOn())
            return true;
        return false;
    }

    private void OnPick(InputValue value)
    {
        if (!_delay)
        {
            UsePick();
        }
    }

    private void OnDash(InputValue value)
    {
        if (!_player.playerStatus.IsDoing[(int)doName.Dash])
        {
            _player.playerStatus.IsDoing[(int)doName.Dash] = true;
            _player.Rigid.AddForce(transform.forward * 8f, ForceMode.Impulse);
            UseDash();
            StartCoroutine(CoolTime((int)doName.Dash, 3f));
        }
    }

    private void OnAttack(InputValue value)
    {
        if (!_delay && !_player.playerStatus.IsOnUI) UseAttack();

    }

    private void OnSkill1(InputValue value)
    {
        if (!_player.playerStatus.IsDoing[(int)doName.Skill1] && !_delay)
        {
            _player.playerStatus.IsDoing[(int)doName.Skill1] = true;
            UseSkill(1);
            StartCoroutine(CoolTime((int)doName.Skill1, 3f));
        }
    }

    private void OnSkill2(InputValue value)
    {
        if (!_player.playerStatus.IsDoing[(int)doName.Skill2] && !_delay)
        {
            _player.playerStatus.IsDoing[(int)doName.Skill2] = true;
            UseSkill(2);
            StartCoroutine(CoolTime((int)doName.Skill2, 3f));
        }
    }

    private void OnSkill3(InputValue value)
    {
        if (!_player.playerStatus.IsDoing[(int)doName.Skill3] && !_delay)
        {
            _player.playerStatus.IsDoing[(int)doName.Skill3] = true;
            _player.Rigid.AddForce(transform.forward, ForceMode.Impulse);
            UseSkill(3);
            StartCoroutine(CoolTime((int)doName.Skill3, 3f));
        }
    }

    private void AttackRange(int attack)
    {
        if (attack == 1) _player.AttackRange.SetActive(true);
        else _player.AttackRange.SetActive(false);
    }

    private void PickRange(int pick)
    {
        if (pick == 1) _player.PickRange.SetActive(true);
        else _player.PickRange.SetActive(false);
    }

    private void OffDelay()
    {
        _delay = false;
        _player.Rigid.velocity = Vector3.zero;
    }

    private void OnNum1(InputValue value)
    {
        if (!_player.playerStatus.IsDoing[(int)doName.Num1])
        {
            _player.playerStatus.IsDoing[(int)doName.Num1] = true;
            Debug.Log("아이템 1 사용");
            StartCoroutine(CoolTime((int)doName.Num1, 3f));
        }
    }

    private void OnNum2(InputValue value)
    {
        if (!_player.playerStatus.IsDoing[(int)doName.Num2])
        {
            _player.playerStatus.IsDoing[(int)doName.Num2] = true;
            Debug.Log("아이템 2 사용");
            StartCoroutine(CoolTime((int)doName.Num2, 3f));
        }
    }

    private void OnNum3(InputValue value)
    {
        if (!_player.playerStatus.IsDoing[(int)doName.Num3])
        {
            _player.playerStatus.IsDoing[(int)doName.Num3] = true;
            Debug.Log("아이템 3 사용");
            StartCoroutine(CoolTime((int)doName.Num3, 3f));
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            _player.playerStatus.IsJump = false;
    }

    private IEnumerator CoolTime(int doNum, float time)
    {
        while (time > 0.0f)
        {
            time -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        _player.playerStatus.IsDoing[doNum] = false;
    }

    private void UseDash()
    {
        _delay = true;
        _player.Animator.SetTrigger("IsDash");
    }

    private void UseJump()
    {
        _player.Animator.SetTrigger("IsJump");
    }

    private void UsePick()
    {
        _delay = true;
        _player.Animator.SetTrigger("IsPick");
    }

    private void UseAttack()
    {
        _delay = true;
        _player.Animator.SetTrigger("IsAttack");
    }

    private void UseSkill(int num)
    {
        _delay = true;
        _player.Animator.SetInteger("SkillNum", num);
        _player.Animator.SetTrigger("IsSkill");
    }

    private void IsMove(bool move)
    {
        _player.Animator.SetBool("IsMove", move);
    }


}
