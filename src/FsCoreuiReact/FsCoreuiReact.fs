module FsCoreuiReact

open Fable.Core
open Fable.Helpers.React
open Fable.Import.React
open Fable.Core.JsInterop

[<AutoOpen>]
module Props =
    type AsideProps =
        | ClassName of string
        | Display of string
        | Fixed of bool
        | IsOpen of bool
        | OffCanvas of bool
        | Tag of string

    type AsideTogglerProps =
        | ClassName of string
        | DefaultOpen of bool
        | Display of string
        | Mobile of bool
        | Tag of string
        | Type of string

    // TODO: see if I can use the Component prop
    type Route =
        { Path : string
          Exact : bool
          Name : string }

    type BreadcrumpProps =
        | ClassName of string
        | Tag of string
        | AppRoutes of Route list

    type FooterProps =
        | ClassName of string
        | Fixed of bool
        | Tag of string

    type HeaderProps =
        | ClassName of string
        | Fixed of bool
        | Tag of string

    type HeaderDropdownProps =
        | Direction of string // TODO find out directions

    type BrandOptions =
        { Src : ReactElement // I think this should be a SVG react element
          Width : int
          Height : int
          Alt : string }

    type NavbarBrandProps =
        | Tag of string
        | ClassName of string
        | Brand of BrandOptions
        | Full of BrandOptions
        | Minimized of BrandOptions

    type SidebarProps =
        | ClassName of string
        | Compact of bool
        | Display of string
        | Fixed of bool
        | Minimized of bool
        | IsOpen of bool
        | OffCanvas of bool
        | Tag of string

    type SidebarFooterProps =
        | ClassName of string
        | Tag of string

    type SidebarFormProps =
        | ClassName of string
        | Tag of string

    type SidebarHeaderProps =
        | ClassName of string
        | Tag of string

    type SidebarMinimizerProps =
        | ClassName of string
        | Tag of string
        | Type of string // TODO have to research which types are allowed

    type BadgeConfig =
        { Variant : string
          Text : string
          Class : string option }

    type NavConfig =
        { Name : string
          Url : string
          Icon : string option
          Class : string option
          Badge : BadgeConfig }

    type SidebarNavProps =
        | ClassName of string
        | IsOpen of bool
        | Tag of string
        | NavConfig of NavConfig

    type SidebarTogglerProps =
        | ClassName of string
        | Display of string
        | Mobile of bool
        | Tag of string
        | Type of string // TODO research all possible types

let inline aside (props : AsideProps list) (elems : ReactElement list) : ReactElement =
    ofImport "AppAside" "@coreui/react" (keyValueList CaseRules.LowerFirst props) elems

let inline asideToggler (props : AsideTogglerProps list) (elems : ReactElement list) : ReactElement =
    ofImport "AppAsideToggler" "@coreui/react" (keyValueList CaseRules.LowerFirst props) elems

let inline breadcrump (props : BreadcrumpProps list) (elems : ReactElement list) : ReactElement =
    ofImport "AppBreadcrump" "@coreui/react" (keyValueList CaseRules.LowerFirst props) elems

let inline footer (props : FooterProps list) (elems : ReactElement list) : ReactElement =
    ofImport "AppFooter" "@coreui/react" (keyValueList CaseRules.LowerFirst props) elems

let inline header (props : HeaderProps list) (elems : ReactElement list) : ReactElement =
    ofImport "AppHeader" "@coreui/react" (keyValueList CaseRules.LowerFirst props) elems

let inline headerDropdown (props : HeaderDropdownProps list) (elems : ReactElement list) : ReactElement =
    ofImport "AppHeaderDropdown" "@coreui/react" (keyValueList CaseRules.LowerFirst props) elems

let inline navbarBrand (props : NavbarBrandProps list) (elems : ReactElement list) : ReactElement =
    ofImport "AppNavbarBrand" "@coreui/react" (keyValueList CaseRules.LowerFirst props) elems

let inline sidebar (props : SidebarProps list) (elems : ReactElement list) : ReactElement =
    ofImport "AppSidebar" "@coreui/react" (keyValueList CaseRules.LowerFirst props) elems

let inline sidebarFooter (props : SidebarFooterProps list) (elems : ReactElement list) : ReactElement =
    ofImport "AppSidebarFooter" "@coreui/react" (keyValueList CaseRules.LowerFirst props) elems

let inline sidebarForm (props : SidebarFormProps list) (elems : ReactElement list) : ReactElement =
    ofImport "AppSidebarForm" "@coreui/react" (keyValueList CaseRules.LowerFirst props) elems

let inline sidebarHeader (props : SidebarHeaderProps list) (elems : ReactElement list) : ReactElement =
    ofImport "AppSidebarHeader" "@coreui/react" (keyValueList CaseRules.LowerFirst props) elems

let inline sidebarMinimizer (props : SidebarMinimizerProps list) (elems : ReactElement list) : ReactElement =
    ofImport "AppSidebarMinimizer" "@coreui/react" (keyValueList CaseRules.LowerFirst props) elems

let inline sidebarNav (props : SidebarNavProps list) (elems : ReactElement list) : ReactElement =
    ofImport "AppSidebarNav" "@coreui/react" (keyValueList CaseRules.LowerFirst props) elems

let inline sidebarToggler (props : SidebarTogglerProps list) (elems : ReactElement list) : ReactElement =
    ofImport "AppSidebarToggler" "@coreui/react" (keyValueList CaseRules.LowerFirst props) elems