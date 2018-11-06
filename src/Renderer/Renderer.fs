module Renderer

open Elmish
open Elmish.React
open Fable.Core.JsInterop
open Fable.Import.React

importAll "./assets/styles/app.scss"

type PageModel =
    | CounterPage of Counter.Model
    | AlertsPage

type ActiveTab =
    | One
    | Two
    | Three

type Model =
    { PageModel : PageModel
      ActiveTab : ActiveTab }

type Msg =
    | CounterMsg of Counter.Message
    | ChangeAsideTab of ActiveTab

let urlUpdate (result : Routing.Route option) model =
    match result with
    | Some Routing.Counter ->
        let mdl = Counter.init ()
        { model with PageModel = CounterPage mdl }, Cmd.none
    | Some Routing.Alerts ->
        { model with PageModel = AlertsPage }, Cmd.none
    | None ->
        model, Routing.newUrl Routing.Counter

let init result =
    let cm = Counter.init ()
    let model = { PageModel = CounterPage cm
                  ActiveTab = One }

    urlUpdate result model

let update (msg : Msg) (model : Model) =
    match msg, model.PageModel with
    | CounterMsg msg, CounterPage mdl ->
        let cm = Counter.update msg mdl
        { model with PageModel = CounterPage cm}, Cmd.none
    | CounterMsg msg, _ -> model, Cmd.none
    | ChangeAsideTab nowActive, _ ->
        { model with ActiveTab = nowActive }, Cmd.none

module R = Fable.Helpers.React
module RP = Fable.Helpers.React.Props
module RS = FsReactstrap
module RSP = FsReactstrap.Props
module RC = FsCoreuiReact
module RCP = FsCoreuiReact.Props

let randomAvatar () =
    let names =
        [ "bobben"; "thommy"; "georg"; "maria"; "clara"; "mahula"; "elif"; "muammer"
          "paul"; "pavel"; "vitezlav"; "jesus"; "thedevil"; "bigKahuna"; "someone"]
    let name = names |> List.sortBy (fun _ -> System.Guid.NewGuid()) |> List.head
    sprintf "https://api.adorable.io/avatars/150/%s.png" name

let viewContent model dispatch =
    match model.PageModel with
    | CounterPage cpm ->
        R.fragment [] [
            Counter.view cpm (CounterMsg >> dispatch)
        ]
    | AlertsPage ->
        R.fragment [] [
            R.h1 [] [ R.str "Hello from alerts" ]
        ]

let brandImage = importAll<ReactElement> "./assets/img/brand/logo.svg"
let smallBrandImage = importAll<ReactElement> "./assets/img/brand/sygnet.svg"

let headerFragment =
    let fullBrandProps : RCP.BrandOptions =
        { src = brandImage
          width = 89
          height = 25
          alt = "CoreUI Logo" }

    let minimizedBrandProps : RCP.BrandOptions =
        { src = smallBrandImage
          width = 30
          height = 30
          alt = "CoreUI Logo" }

    R.fragment [] [
        RC.sidebarToggler
            [ RCP.SidebarTogglerProps.ClassName "d-lg-none"
              RCP.SidebarTogglerProps.Display "md"
              RCP.SidebarTogglerProps.Mobile true ] []
        RC.navbarBrand [ RCP.Brand fullBrandProps; RCP.Minimized minimizedBrandProps ] []
        RC.sidebarToggler
            [ RCP.SidebarTogglerProps.ClassName "d-md-down-none"
              RCP.SidebarTogglerProps.Display "lg" ] []
        
        RS.nav [ RP.ClassName "d-md-down-none"; RSP.Navbar ] [
            RS.navItem [ RP.ClassName "px-3" ] [
                RS.navLink [ Routing.href Routing.Counter ] [ R.str "Dashboard" ]
            ]
            RS.navItem [ RP.ClassName "px-3" ] [
                RS.navLink [ Routing.href Routing.Alerts ] [ R.str "Users" ]
            ]
            RS.navItem [ RP.ClassName "px-3" ] [
                RS.navLink [ RP.Href "#" ] [ R.str "Settings" ]
            ]
        ]

        RS.nav [ RP.ClassName "ml-auto"; RSP.Navbar ] [
            RS.navItem [ RP.ClassName "d-md-down-none" ] [
                RS.navLink [ RP.Href "#" ] [
                    R.i [ RP.ClassName "icon-bell" ] []
                    RS.badge [ RSP.Pill true; RSP.RSBadgeProps.Color RSP.Danger ] [ R.str "5" ]
                ]
            ]
            RS.navItem [ RP.ClassName "d-md-down-none" ] [
                RS.navLink [ RP.Href "#" ] [
                    R.i [ RP.ClassName "icon-list" ] []
                ]
            ]
            RS.navItem [ RP.ClassName "d-md-down-none" ] [
                RS.navLink [ RP.Href "#" ] [
                    R.i [ RP.ClassName "icon-location-pin" ] []
                ]
            ]

            RC.headerDropdown [ RCP.Direction "down" ] [
                RS.dropdownToggle [ RSP.RSDropdownToggleProps.Nav ] [
                    R.img [ RP.Src (randomAvatar ()); RP.ClassName "img-avatar" ]
                ]
                RS.dropdownMenu [ RSP.Right ] [
                    RS.dropdownItem [ RSP.Header; RSP.RSDropdownItemProps.Tag "div"; RSP.RSDropdownItemProps.ClassName "text-center" ] [
                        R.strong [] [ R.str "Account" ]
                    ]
                    RS.dropdownItem [ ] [
                        R.i [ RP.ClassName "fa fa-bell-o" ] []
                        R.str " Updates"
                        RS.badge [ RSP.RSBadgeProps.Color RSP.Info ] [ R.str "42" ]
                    ]
                    RS.dropdownItem [ ] [
                        R.i [ RP.ClassName "fa fa-envelope-o" ] []
                        R.str " Messages"
                        RS.badge [ RSP.RSBadgeProps.Color RSP.Success ] [ R.str "42" ]
                    ]
                    RS.dropdownItem [ ] [
                        R.i [ RP.ClassName "fa fa-tasks" ] []
                        R.str " Tasks"
                        RS.badge [ RSP.RSBadgeProps.Color RSP.Danger ] [ R.str "42" ]
                    ]
                    RS.dropdownItem [ ] [
                        R.i [ RP.ClassName "fa fa-comments" ] []
                        R.str " Comments"
                        RS.badge [ RSP.RSBadgeProps.Color RSP.Warning ] [ R.str "42" ]
                    ]
                    RS.dropdownItem
                        [ RSP.RSDropdownItemProps.Tag "div"
                          RSP.Header
                          RSP.RSDropdownItemProps.ClassName "text-center" ] [
                              R.strong [] [ R.str "Settings" ]
                    ]
                    RS.dropdownItem [ ] [
                        R.i [ RP.ClassName "fa fa-user" ] []
                        R.str " Profile"
                    ]
                    RS.dropdownItem [ ] [
                        R.i [ RP.ClassName "fa fa-wrench" ] []
                        R.str " Settings"
                    ]
                    RS.dropdownItem [ ] [
                        R.i [ RP.ClassName "fa fa-usd" ] []
                        R.str " Payment"
                    ]
                    RS.dropdownItem [ ] [
                        R.i [ RP.ClassName "fa fa-file" ] []
                        R.str " Projects"
                    ]
                    RS.dropdownItem [ RSP.Divider ] []
                    RS.dropdownItem [ ] [
                        R.i [ RP.ClassName "fa fa-shield" ] []
                        R.str " Lock Account"
                    ]
                    RS.dropdownItem [ ] [
                        R.i [ RP.ClassName "fa fa-lock" ] []
                        R.str " Logout"
                    ]
                ]
            ]
        ]
        RC.asideToggler [ RCP.AsideTogglerProps.ClassName "d-md-down-none" ] []
    ]

let mainAside model dispatch =
    let (one, two, three) =
        match model.ActiveTab with
        | One -> true, false, false
        | Two -> false, true, false
        | Three -> false, false, true

    let isActive active : RP.IProp list =
        if active then [ RP.ClassName "active"] else []

    let tabToString (tab : ActiveTab) =
        match tab with
        | One -> "one"
        | Two -> "two"
        | Three -> "three"

    R.fragment [] [
        RS.nav [ RSP.Tabs ] [
            RS.navLink
                [ yield! (isActive one)
                  yield RP.OnClick (fun _ -> ChangeAsideTab One |> dispatch ) ] [
                R.i [ RP.ClassName "icon-list" ] []
            ]
            RS.navLink
                [ yield! (isActive two)
                  yield RP.OnClick (fun _ -> ChangeAsideTab Two |> dispatch ) ] [
                R.i [ RP.ClassName "icon-speech" ] []
            ]
            RS.navLink
                [ yield! (isActive three)
                  yield RP.OnClick (fun _ -> ChangeAsideTab Three |> dispatch ) ] [
                R.i [ RP.ClassName "icon-settings" ] []
            ]
        ]
        RS.tabContent [ RSP.ActiveTab (tabToString model.ActiveTab) ] [
            RS.tabPane [ RSP.TabId (tabToString One) ] [
                RS.listGroup
                    [ RSP.RSListGroupProps.ClassName "list-group-accent"
                      RSP.RSListGroupProps.Tag "div" ] [
                      RS.listGroupItem
                        [ RSP.RSListGroupItemProps.ClassName "list-group-item-accent-secondary bg-light text-center font-weight-bold text-muted text-uppercase small" ] [
                            R.str "Today"
                      ]
                      RS.listGroupItem
                        [ RSP.Action
                          RSP.RSListGroupItemProps.Tag "a"
                          RP.Href "#"
                          RP.ClassName "list-group-item-accent-warning list-group-item-divider" ] [
                              R.div [ RP.ClassName "avatar float-right" ] [
                                  R.img [ RP.ClassName "img-avatar"
                                          RP.Src (randomAvatar ())
                                          RP.Alt "admin@bootstrapmaster.com" ]
                              ]
                              R.div [] [
                                  R.str "Meeting with"
                                  R.strong [ ] [ R.str " Lucas" ]
                              ]
                              R.small [ RP.ClassName "text-muted mr-3" ] [
                                  R.i [ RP.ClassName "icon-calendar" ] [ ]
                                  R.str "  1 - 3pm"
                              ]
                              R.small [ RP.ClassName "text-muted" ] [
                                  R.i [ RP.ClassName "icon-location-pin" ] [ ]
                                  R.str " Palo Alto, CA"
                              ]
                      ]
                ]
            ]
            RS.tabPane [ RSP.TabId (tabToString Two) ] [
                R.h4 [] [ R.str "two" ]
            ]
            RS.tabPane [ RSP.TabId (tabToString Three) ] [
                R.h4 [] [ R.str "three" ]
            ]
        ]
    ]

let view (model : Model) (dispatch : Msg -> unit) =
    R.div [ RP.ClassName "app" ] [
        RC.header [ RCP.HeaderProps.Fixed true ] [ headerFragment ]
        R.div [ RP.ClassName "app-body" ] [
            RC.sidebar [] [
                RC.sidebarHeader [] []
                RC.sidebarForm [] []
                RC.sidebarFooter [] []
                RC.sidebarMinimizer [] []
            ]
            R.main [ RP.ClassName "main" ] [
                RS.container [ RSP.Fluid true; RP.Style [ RP.PaddingTop 20 ] ] [
                    viewContent model dispatch
                ]
            ]
            RC.aside [ RCP.Fixed true ] [
                mainAside model dispatch
            ]
        ]
        RC.footer [] []
    ]

#if DEBUG
open Elmish.HMR
open Elmish.Debug
#endif

open Elmish.Browser.Navigation
open Elmish.Browser.UrlParser

Program.mkProgram init update view
|> Program.toNavigable (parseHash Routing.route) urlUpdate
#if DEBUG
|> Program.withHMR
|> Program.withDebugger
#endif
|> Program.withReact "app"
#if DEBUG
|> Program.withConsoleTrace
#endif
|> Program.run
