// GENERATED AUTOMATICALLY FROM 'Assets/InputSystem/InputController.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputController : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputController"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""2a5ad580-f3d4-46c3-aeba-aee21c1193cb"",
            ""actions"": [
                {
                    ""name"": ""HorizontalMove"",
                    ""type"": ""Button"",
                    ""id"": ""64f20641-bb19-4924-9a2a-5af2cee790b6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""VerticalMove"",
                    ""type"": ""Button"",
                    ""id"": ""00a3d68d-8e1d-4737-b639-5aadbb1f5d13"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""CharacterAndInventory"",
                    ""type"": ""Button"",
                    ""id"": ""59bfabb0-cba5-4036-9045-10f330a085cd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Interaction"",
                    ""type"": ""Button"",
                    ""id"": ""b0b7501c-2f02-4af1-85ce-1698c8a57cd6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""MainSkill"",
                    ""type"": ""Button"",
                    ""id"": ""e0412d86-fb83-48ca-a3bc-ee03255e0331"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""SecondSkill"",
                    ""type"": ""Button"",
                    ""id"": ""6f0b8b2a-e116-4b61-bc20-e99b25bf851f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""CancelAction"",
                    ""type"": ""Button"",
                    ""id"": ""dfa98787-0ede-48b5-908d-6257ea28ccdd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Arrows"",
                    ""id"": ""50a2174b-e89a-4701-aaf2-b09dfc2596ed"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""9e974c3b-16c8-46df-bab1-4c0789c213ce"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""HorizontalMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""453c04da-452c-46c4-9a73-7e1a26425915"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""HorizontalMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""c7acf694-a58d-4fdb-8fd1-44d6f6f04319"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""802bf662-4b85-4652-8d13-644ad3949ce8"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""HorizontalMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""9b5e50a6-5605-45e0-bd64-b329df482d77"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""HorizontalMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""40946894-069b-4d51-9fb3-175564e88ba4"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""VerticalMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""50a37d17-c7ee-4230-85f1-9598082eeb65"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""VerticalMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""a5ba4a86-ff4c-4076-adc8-08cacc943737"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""VerticalMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""d8e158b9-4cb6-43e2-9ede-25137aba15b0"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VerticalMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""5fba5d5e-fb1a-4438-ab0b-08492498d418"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""VerticalMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""0b10c232-de76-4929-ba9f-c9d3b45e763b"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""VerticalMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3cda3575-e090-47be-b6e5-8ce80b38d7bc"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""CharacterAndInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Interaction"",
                    ""id"": ""17a791f0-1e58-4170-9554-d550521891d1"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interaction"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""46e1e6a3-eeac-46ce-9bab-db6102886479"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""1f558c2d-30fb-4d32-bf57-0ef4e51fb845"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""f20bd36d-9dc1-4407-bb5e-b05e4a0d60da"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MainSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d7b4fb2c-8bb9-4fa9-bb77-4bb4d8bcee71"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SecondSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea561ec2-d540-42af-a533-71b52fef177c"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""CancelAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": []
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_HorizontalMove = m_Player.FindAction("HorizontalMove", throwIfNotFound: true);
        m_Player_VerticalMove = m_Player.FindAction("VerticalMove", throwIfNotFound: true);
        m_Player_CharacterAndInventory = m_Player.FindAction("CharacterAndInventory", throwIfNotFound: true);
        m_Player_Interaction = m_Player.FindAction("Interaction", throwIfNotFound: true);
        m_Player_MainSkill = m_Player.FindAction("MainSkill", throwIfNotFound: true);
        m_Player_SecondSkill = m_Player.FindAction("SecondSkill", throwIfNotFound: true);
        m_Player_CancelAction = m_Player.FindAction("CancelAction", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_HorizontalMove;
    private readonly InputAction m_Player_VerticalMove;
    private readonly InputAction m_Player_CharacterAndInventory;
    private readonly InputAction m_Player_Interaction;
    private readonly InputAction m_Player_MainSkill;
    private readonly InputAction m_Player_SecondSkill;
    private readonly InputAction m_Player_CancelAction;
    public struct PlayerActions
    {
        private @InputController m_Wrapper;
        public PlayerActions(@InputController wrapper) { m_Wrapper = wrapper; }
        public InputAction @HorizontalMove => m_Wrapper.m_Player_HorizontalMove;
        public InputAction @VerticalMove => m_Wrapper.m_Player_VerticalMove;
        public InputAction @CharacterAndInventory => m_Wrapper.m_Player_CharacterAndInventory;
        public InputAction @Interaction => m_Wrapper.m_Player_Interaction;
        public InputAction @MainSkill => m_Wrapper.m_Player_MainSkill;
        public InputAction @SecondSkill => m_Wrapper.m_Player_SecondSkill;
        public InputAction @CancelAction => m_Wrapper.m_Player_CancelAction;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @HorizontalMove.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHorizontalMove;
                @HorizontalMove.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHorizontalMove;
                @HorizontalMove.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHorizontalMove;
                @VerticalMove.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnVerticalMove;
                @VerticalMove.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnVerticalMove;
                @VerticalMove.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnVerticalMove;
                @CharacterAndInventory.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCharacterAndInventory;
                @CharacterAndInventory.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCharacterAndInventory;
                @CharacterAndInventory.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCharacterAndInventory;
                @Interaction.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteraction;
                @Interaction.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteraction;
                @Interaction.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteraction;
                @MainSkill.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMainSkill;
                @MainSkill.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMainSkill;
                @MainSkill.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMainSkill;
                @SecondSkill.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondSkill;
                @SecondSkill.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondSkill;
                @SecondSkill.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondSkill;
                @CancelAction.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCancelAction;
                @CancelAction.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCancelAction;
                @CancelAction.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCancelAction;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @HorizontalMove.started += instance.OnHorizontalMove;
                @HorizontalMove.performed += instance.OnHorizontalMove;
                @HorizontalMove.canceled += instance.OnHorizontalMove;
                @VerticalMove.started += instance.OnVerticalMove;
                @VerticalMove.performed += instance.OnVerticalMove;
                @VerticalMove.canceled += instance.OnVerticalMove;
                @CharacterAndInventory.started += instance.OnCharacterAndInventory;
                @CharacterAndInventory.performed += instance.OnCharacterAndInventory;
                @CharacterAndInventory.canceled += instance.OnCharacterAndInventory;
                @Interaction.started += instance.OnInteraction;
                @Interaction.performed += instance.OnInteraction;
                @Interaction.canceled += instance.OnInteraction;
                @MainSkill.started += instance.OnMainSkill;
                @MainSkill.performed += instance.OnMainSkill;
                @MainSkill.canceled += instance.OnMainSkill;
                @SecondSkill.started += instance.OnSecondSkill;
                @SecondSkill.performed += instance.OnSecondSkill;
                @SecondSkill.canceled += instance.OnSecondSkill;
                @CancelAction.started += instance.OnCancelAction;
                @CancelAction.performed += instance.OnCancelAction;
                @CancelAction.canceled += instance.OnCancelAction;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnHorizontalMove(InputAction.CallbackContext context);
        void OnVerticalMove(InputAction.CallbackContext context);
        void OnCharacterAndInventory(InputAction.CallbackContext context);
        void OnInteraction(InputAction.CallbackContext context);
        void OnMainSkill(InputAction.CallbackContext context);
        void OnSecondSkill(InputAction.CallbackContext context);
        void OnCancelAction(InputAction.CallbackContext context);
    }
}
