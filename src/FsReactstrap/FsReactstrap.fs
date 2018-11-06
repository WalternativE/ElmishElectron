module FsReactstrap

open Fable.Core
open Fable.Helpers.React
open Fable.Import.React
open Fable.Core.JsInterop
open Fable.Helpers.React.Props

// TODO investigate if bool props can be simplified with normal DU cases
[<AutoOpen>]
module Props =
    [<StringEnum>]
    type Color =
        | Primary
        | Secondary
        | Success
        | Danger
        | Warning
        | Info
        | Light
        | Dark

    type RSButtonProps =
        | Active of bool
        | Block of bool
        | Color of Color
            interface IProp

    [<StringEnum>]
    type Fluid =
        | [<CompiledName("container-fluid")>] ContainerFluid
        | Container

    type RSContainerProps =
        | Fluid of bool
            interface IProp

    type RSAlertProps =
        | CloseClassName of string
        | Color of Color
        | IsOpen of bool
        | Toggle of (MouseEvent -> unit)
        | Tag of string // TODO find out what the optional function type could mean
        // TODO implement Fade component
            interface IProp

    type RSBadgeProps =
        | Color of Color
        | Pill of bool
            interface IProp

    [<StringEnum>]
    type Fixed =
        | Top
        | Bottom

    type RSNavbarProps =
        | Light of bool
        | Dark of bool
        | Fixed of Fixed
        | Color of Color
        | Role of string // e.g. navigation
        | Expand of string // TODO research possible values
            interface IProp

    type RSNavProps =
        | Tabs
        | Pills
        | Card
        | Justified
        | Fill
        | Vertical
        | Horizontal
        | Navbar
        | Tag of string
            interface IProp

    type RSNavItemProps =
        | Active
            interface IProp

    type RSNavLinkProps =
        | Disabled
        | Active
            interface IProp

    type RSCollapseProps =
        | IsOpen of bool
        | Navbar
            interface IProp

    [<StringEnum>]
    type InputType =
        | Text

    type RSInputProps =
        | Type of InputType
        | Size of string // research values
        | BsSize of string // research values
        | Valid
        | Invalid
        | Plaintext
        | Addon
            interface IProp

    type RSRowProps =
        | NoGutters
        | Form

    type RSColProps =
        | Xs of string
        | Sm of string
        | Md of string
        | Lg of string
        | Xl of string

    type RSDropdownToggleProps =
        | Caret
        | Color of Color
        | ClassName of string
        | Disabled
        | OnClick of (unit -> unit) // TODO research method signature
        | [<CompiledName("aria-haspopup")>]AriaHasPopup
        | Split
        | Nav

    type RSDropdownMenuProps =
        | Tag of string
        | Right
        | Flip
        | Persist

    type RSDropdownItemProps =
        | Active
        | Disabled
        | Divider
        | Tag of string
        | Header
        | OnClick of (unit -> unit) // TODO research method signature
        | ClassName of string
        | Toggle

    type RSTabContentProps =
        | Tag of string
        | ActiveTab of string // TODO maybe make this another object type
        | ClassName of string

    type RSTabPaneProps =
        | Tag of string
        | ClassName of string
        | TabId of string

    type RSListGroupProps =
        | Flush
        | ClassName of string
        | Tag of string

    type RSListGroupItemProps =
        | Tag of string
        | Active
        | Disabled
        | Color of Color
        | Action
        | ClassName of string
            interface IProp

let inline alert (props : IProp list) (elems : ReactElement list) : ReactElement =
    ofImport "Alert" "reactstrap" (keyValueList CaseRules.LowerFirst props) elems

let inline button (props : IProp list) (elems : ReactElement list) : ReactElement =
    ofImport "Button" "reactstrap" (keyValueList CaseRules.LowerFirst props) elems

let inline container (props : IProp list) (elems : ReactElement list) : ReactElement =
    ofImport "Container" "reactstrap" (keyValueList CaseRules.LowerFirst props) elems

let inline badge (props : IProp list) (elems : ReactElement list) : ReactElement =
    ofImport "Badge" "reactstrap" (keyValueList CaseRules.LowerFirst props) elems

let inline navbar (props : IProp list) (elems : ReactElement list) : ReactElement =
    ofImport "Navbar" "reactstrap" (keyValueList CaseRules.LowerFirst props) elems

let inline navbarBrand (props : IProp list) (elems : ReactElement list) : ReactElement =
    ofImport "NavbarBrand" "reactstrap" (keyValueList CaseRules.LowerFirst props) elems

let inline nav (props : IProp list) (elems : ReactElement list) : ReactElement =
    ofImport "Nav" "reactstrap" (keyValueList CaseRules.LowerFirst props) elems

let inline navItem (props : IProp list) (elems : ReactElement list) : ReactElement =
    ofImport "NavItem" "reactstrap" (keyValueList CaseRules.LowerFirst props) elems

let inline navLink (props : IProp list) (elems : ReactElement list) : ReactElement =
    ofImport "NavLink" "reactstrap" (keyValueList CaseRules.LowerFirst props) elems

let inline collapse (props : IProp list) (elems : ReactElement list) : ReactElement =
    ofImport "Collapse" "reactstrap" (keyValueList CaseRules.LowerFirst props) elems

let inline input (props : IProp list) : ReactElement =
    ofImport "Input" "reactstrap" (keyValueList CaseRules.LowerFirst props) []

let inline row (props : RSRowProps list) (elems : ReactElement list) : ReactElement =
    ofImport "Row" "reactstrap" (keyValueList CaseRules.LowerFirst props) elems

let inline col (props : RSColProps list) (elems : ReactElement list) : ReactElement =
    ofImport "Col" "reactstrap" (keyValueList CaseRules.LowerFirst props) elems

let inline dropdownToggle (props : RSDropdownToggleProps list) (elems : ReactElement list) : ReactElement =
    ofImport "DropdownToggle" "reactstrap" (keyValueList CaseRules.LowerFirst props) elems

let inline dropdownMenu (props : RSDropdownMenuProps list) (elems : ReactElement list) : ReactElement =
    ofImport "DropdownMenu" "reactstrap" (keyValueList CaseRules.LowerFirst props) elems

let inline dropdownItem (props : RSDropdownItemProps list) (elems : ReactElement list) : ReactElement =
    ofImport "DropdownItem" "reactstrap" (keyValueList CaseRules.LowerFirst props) elems

let inline tabContent (props : RSTabContentProps list) (elems : ReactElement list) : ReactElement =
    ofImport "TabContent" "reactstrap" (keyValueList CaseRules.LowerFirst props) elems

let inline tabPane (props : RSTabPaneProps list) (elems : ReactElement list) : ReactElement =
    ofImport "TabPane" "reactstrap" (keyValueList CaseRules.LowerFirst props) elems

let inline listGroup (props : RSListGroupProps list) (elems : ReactElement list) : ReactElement =
    ofImport "ListGroup" "reactstrap" (keyValueList CaseRules.LowerFirst props) elems

let inline listGroupItem (props : IProp list) (elems : ReactElement list) : ReactElement =
    ofImport "ListGroupItem" "reactstrap" (keyValueList CaseRules.LowerFirst props) elems