module Main

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Electron
open Electron.Electron
open Fable.PowerPack

type ExtensionReference =
    { id : string
      electron : string }

type InstallExtension = (U2<ExtensionReference, ExtensionReference array> -> JS.Promise<string>)

[<Import("*", "fs")>]
let fs : Node.Fs.IExports = jsNative

[<Import("*", "path")>]
let path : Node.Path.IExports = jsNative

[<Import("REACT_DEVELOPER_TOOLS", "electron-devtools-installer")>]
let reactDeveloperTools : ExtensionReference = jsNative

[<Import("REDUX_DEVTOOLS", "electron-devtools-installer")>]
let reduxDeveloperTools : ExtensionReference = jsNative

let installExtension = importDefault<InstallExtension> "electron-devtools-installer"

type IUrl<'a> =
    inherit JsConstructor<string, Node.Url.Url<'a>>

let url = importMember<Node.Url.Url<string>> "url"

[<Import("URL", "url")>]
let URL : IUrl<string> = jsNative

let createProtocol (scheme : string) =
    Electron.protocol.registerBufferProtocol(
        scheme,
        (fun req resp ->
            let url = URL.Create req.url
            let pathName = JS.decodeURI url.pathname.Value

            let filePath = path.join [| Node.Globals.__dirname; pathName |]

            fs.readFile(
                filePath,
                (fun err data ->
                    match err with
                    | Some e ->
                        JS.console.error (sprintf "Failure to read file for scheme %s" scheme)
                        e.stack
                        |> Option.iter (fun s -> (JS.console.error (sprintf "Stacktrace:\n%s" s)))
                    | None ->
                        let extension = path.extname(pathName).ToLower()
                        let mimeType =
                            match extension with
                            | _ when extension = ".js" -> "text/javascript"
                            | _ when extension = ".html" -> "text/html"
                            | _ when extension = ".css" -> "text/css"
                            | _ when extension = ".svg" || extension = ".svgz" -> "image/svg+xml"
                            | _ when extension = ".json" -> "application/json"
                            | _ -> ""
                        
                        let mimeTypedBuffer = createEmpty<MimeTypedBuffer>
                        mimeTypedBuffer.mimeType <- mimeType
                        mimeTypedBuffer.data <- !!data

                        resp !!mimeTypedBuffer )
            )
        ),
        (fun err ->
            if err <> null then
                JS.console.error (sprintf "Failure to register scheme %s: %A" scheme err)
            else ())
    )

// Keep a global reference of the window object, if you don't, the window will
// be closed automatically when the JavaScript object is garbage collected.
let mutable mainWindow: BrowserWindow option = Option.None

let options = createEmpty<RegisterStandardSchemesOptions>
options.secure <- Some true

Electron.protocol.registerStandardSchemes(ResizeArray<_>(["app"]), options)
let createMainWindow () =
    let options = createEmpty<BrowserWindowConstructorOptions>
    options.autoHideMenuBar <- Some false

    #if DEBUG
    options.show <- Some false
    #else
    options.show <- Some true
    #endif

    let window = Electron.BrowserWindow.Create(options)
    window.maximize ()

    window.once(
        "ready-to-show",
        (unbox (fun () -> window.show ()))
    ) |> ignore

    #if DEBUG
    window.loadURL("http://localhost:9000/index.html")
    #else
    createProtocol "app"
    window.loadFile("app/index.html")
    #endif

    // Emitted when the window is closed.
    window.on("closed", unbox(fun () ->
        // Dereference the window object, usually you would store windows
        // in an array if your app supports multi windows, this is the time
        // when you should delete the corresponding element.
        mainWindow <- Option.None
    )) |> ignore

    #if DEBUG
    installExtension !![| reactDeveloperTools; reduxDeveloperTools |]
    |> Promise.either
        (fun name ->
            printfn "Installed %s" name
            window.webContents.openDevTools()
            !!())
        (fun err ->
            printfn "Error happend"
            printfn "%A" err
            !!())
    |> Promise.start
    #endif

    mainWindow <- Some window

// This method will be called when Electron has finished
// initialization and is ready to create browser windows.
Electron.app.on("ready", unbox createMainWindow) |> ignore

// Quit when all windows are closed.
Electron.app.on("window-all-closed", unbox(fun () ->
    // On OS X it is common for applications and their menu bar
    // to stay active until the user quits explicitly with Cmd + Q
    if Node.Globals.``process``.platform <> Node.Base.NodeJS.Darwin then
        Electron.app.quit()
)) |> ignore

Electron.app.on("activate", unbox(fun () ->
    // On OS X it's common to re-create a window in the app when the
    // dock icon is clicked and there are no other windows open.
    if mainWindow.IsNone then
        createMainWindow()
)) |> ignore
