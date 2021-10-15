// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""PlayerInGameButtons"",
            ""id"": ""961a2d73-e769-40a4-9d95-e39f299496d8"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""5cae10c0-7f17-4584-8e68-fe437833092a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""78d406f2-34ad-4369-944e-dec422a7721c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""f47b89c2-5977-466c-8794-7dda5509ba08"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AttackHold"",
                    ""type"": ""Button"",
                    ""id"": ""ab885bee-120f-4c55-aa1d-d16b69b961af"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""bcb61685-98ad-4d44-8669-8f192e80d003"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""bd844b56-e1d7-48b5-9506-47d3d5173660"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""New control scheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""11bbaf1b-9594-45cd-9ee2-8481238d0b3c"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""New control scheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""34bb5422-b971-450b-8bba-b1f64c36f360"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""New control scheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6a435b43-70d1-4121-8625-486c54433a31"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""New control scheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d5d15ab2-79c8-45ce-ac55-fc98a67d1822"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""New control scheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""117a548b-bf09-4fb2-b6b2-d2b2cc9c83c1"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""63cdb196-403e-451c-94e4-01625defacf9"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f8d5457e-2b25-47a5-b846-5d8626687d2b"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AttackHold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""New control scheme"",
            ""bindingGroup"": ""New control scheme"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerInGameButtons
        m_PlayerInGameButtons = asset.FindActionMap("PlayerInGameButtons", throwIfNotFound: true);
        m_PlayerInGameButtons_Move = m_PlayerInGameButtons.FindAction("Move", throwIfNotFound: true);
        m_PlayerInGameButtons_Sprint = m_PlayerInGameButtons.FindAction("Sprint", throwIfNotFound: true);
        m_PlayerInGameButtons_Attack = m_PlayerInGameButtons.FindAction("Attack", throwIfNotFound: true);
        m_PlayerInGameButtons_AttackHold = m_PlayerInGameButtons.FindAction("AttackHold", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // PlayerInGameButtons
    private readonly InputActionMap m_PlayerInGameButtons;
    private IPlayerInGameButtonsActions m_PlayerInGameButtonsActionsCallbackInterface;
    private readonly InputAction m_PlayerInGameButtons_Move;
    private readonly InputAction m_PlayerInGameButtons_Sprint;
    private readonly InputAction m_PlayerInGameButtons_Attack;
    private readonly InputAction m_PlayerInGameButtons_AttackHold;
    public struct PlayerInGameButtonsActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerInGameButtonsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerInGameButtons_Move;
        public InputAction @Sprint => m_Wrapper.m_PlayerInGameButtons_Sprint;
        public InputAction @Attack => m_Wrapper.m_PlayerInGameButtons_Attack;
        public InputAction @AttackHold => m_Wrapper.m_PlayerInGameButtons_AttackHold;
        public InputActionMap Get() { return m_Wrapper.m_PlayerInGameButtons; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerInGameButtonsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerInGameButtonsActions instance)
        {
            if (m_Wrapper.m_PlayerInGameButtonsActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerInGameButtonsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerInGameButtonsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerInGameButtonsActionsCallbackInterface.OnMove;
                @Sprint.started -= m_Wrapper.m_PlayerInGameButtonsActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_PlayerInGameButtonsActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_PlayerInGameButtonsActionsCallbackInterface.OnSprint;
                @Attack.started -= m_Wrapper.m_PlayerInGameButtonsActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerInGameButtonsActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerInGameButtonsActionsCallbackInterface.OnAttack;
                @AttackHold.started -= m_Wrapper.m_PlayerInGameButtonsActionsCallbackInterface.OnAttackHold;
                @AttackHold.performed -= m_Wrapper.m_PlayerInGameButtonsActionsCallbackInterface.OnAttackHold;
                @AttackHold.canceled -= m_Wrapper.m_PlayerInGameButtonsActionsCallbackInterface.OnAttackHold;
            }
            m_Wrapper.m_PlayerInGameButtonsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @AttackHold.started += instance.OnAttackHold;
                @AttackHold.performed += instance.OnAttackHold;
                @AttackHold.canceled += instance.OnAttackHold;
            }
        }
    }
    public PlayerInGameButtonsActions @PlayerInGameButtons => new PlayerInGameButtonsActions(this);
    private int m_NewcontrolschemeSchemeIndex = -1;
    public InputControlScheme NewcontrolschemeScheme
    {
        get
        {
            if (m_NewcontrolschemeSchemeIndex == -1) m_NewcontrolschemeSchemeIndex = asset.FindControlSchemeIndex("New control scheme");
            return asset.controlSchemes[m_NewcontrolschemeSchemeIndex];
        }
    }
    public interface IPlayerInGameButtonsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnAttackHold(InputAction.CallbackContext context);
    }
}
