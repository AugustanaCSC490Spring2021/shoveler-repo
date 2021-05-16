// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerControls.inputactions'

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
            ""name"": ""Land"",
            ""id"": ""83d58189-2866-44d3-bec1-19beb9ea1ac1"",
            ""actions"": [
                {
                    ""name"": ""MoveRight"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1a338a9c-95a1-49f7-a305-374ab193a567"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveForward"",
                    ""type"": ""PassThrough"",
                    ""id"": ""40bb0013-1680-4620-afac-c37671ec6a1f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c2dc176a-1869-4245-8cb2-ce5776bf6f49"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""FiringPosition"",
                    ""type"": ""Value"",
                    ""id"": ""14ee9a2c-50f4-442e-9cbf-e81b84e744b4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""ab5c365c-f8b9-4c08-9106-334cb29c0b91"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""xaxis"",
                    ""id"": ""27a09a3b-4130-4f77-97b8-f814cac5d211"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveRight"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""caccecef-9aa8-433f-94fe-cfe7de3520d4"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""44134bef-35af-440a-ab7e-6462b77ca1ad"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""zaxis"",
                    ""id"": ""8ccf839b-8309-4b8b-a881-43c4c2101ae3"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveForward"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""4ff10337-16cc-49b3-b9b5-3c9eec519e57"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""e8e1f682-bd6b-4634-858d-8100936f296d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""047e7ea4-7519-43c7-a3b3-dc364352b4de"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""20d21f70-a7bc-4e58-b6b1-a53f0df66c55"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FiringPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""95796d1d-d436-4c9d-8339-699730463ee3"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Land
        m_Land = asset.FindActionMap("Land", throwIfNotFound: true);
        m_Land_MoveRight = m_Land.FindAction("MoveRight", throwIfNotFound: true);
        m_Land_MoveForward = m_Land.FindAction("MoveForward", throwIfNotFound: true);
        m_Land_Fire = m_Land.FindAction("Fire", throwIfNotFound: true);
        m_Land_FiringPosition = m_Land.FindAction("FiringPosition", throwIfNotFound: true);
        m_Land_Interact = m_Land.FindAction("Interact", throwIfNotFound: true);
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

    // Land
    private readonly InputActionMap m_Land;
    private ILandActions m_LandActionsCallbackInterface;
    private readonly InputAction m_Land_MoveRight;
    private readonly InputAction m_Land_MoveForward;
    private readonly InputAction m_Land_Fire;
    private readonly InputAction m_Land_FiringPosition;
    private readonly InputAction m_Land_Interact;
    public struct LandActions
    {
        private @PlayerControls m_Wrapper;
        public LandActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveRight => m_Wrapper.m_Land_MoveRight;
        public InputAction @MoveForward => m_Wrapper.m_Land_MoveForward;
        public InputAction @Fire => m_Wrapper.m_Land_Fire;
        public InputAction @FiringPosition => m_Wrapper.m_Land_FiringPosition;
        public InputAction @Interact => m_Wrapper.m_Land_Interact;
        public InputActionMap Get() { return m_Wrapper.m_Land; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LandActions set) { return set.Get(); }
        public void SetCallbacks(ILandActions instance)
        {
            if (m_Wrapper.m_LandActionsCallbackInterface != null)
            {
                @MoveRight.started -= m_Wrapper.m_LandActionsCallbackInterface.OnMoveRight;
                @MoveRight.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnMoveRight;
                @MoveRight.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnMoveRight;
                @MoveForward.started -= m_Wrapper.m_LandActionsCallbackInterface.OnMoveForward;
                @MoveForward.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnMoveForward;
                @MoveForward.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnMoveForward;
                @Fire.started -= m_Wrapper.m_LandActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnFire;
                @FiringPosition.started -= m_Wrapper.m_LandActionsCallbackInterface.OnFiringPosition;
                @FiringPosition.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnFiringPosition;
                @FiringPosition.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnFiringPosition;
                @Interact.started -= m_Wrapper.m_LandActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_LandActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveRight.started += instance.OnMoveRight;
                @MoveRight.performed += instance.OnMoveRight;
                @MoveRight.canceled += instance.OnMoveRight;
                @MoveForward.started += instance.OnMoveForward;
                @MoveForward.performed += instance.OnMoveForward;
                @MoveForward.canceled += instance.OnMoveForward;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @FiringPosition.started += instance.OnFiringPosition;
                @FiringPosition.performed += instance.OnFiringPosition;
                @FiringPosition.canceled += instance.OnFiringPosition;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public LandActions @Land => new LandActions(this);
    public interface ILandActions
    {
        void OnMoveRight(InputAction.CallbackContext context);
        void OnMoveForward(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnFiringPosition(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
}
