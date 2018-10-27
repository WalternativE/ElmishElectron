module Main

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Node.Exports
open Electron
open Electron.Electron

// Keep a global reference of the window object, if you don't, the window will
// be closed automatically when the JavaScript object is garbage collected.
let mutable mainWindow: BrowserWindow option = Option.None

let createMainWindow () =
    let options = createEmpty<BrowserWindowConstructorOptions>
    options.width <- Some 800.
    options.height <- Some 600.
    options.autoHideMenuBar <- Some true
    let window = Electron.BrowserWindow.Create(options)

    #if RELEASE
    // Load the index.html of the app.
    let opts = createEmpty<Node.Url.Url<obj>>
    opts.pathname <- Some <| path.join(Node.Globals.__dirname, "index.html")
    opts.protocol <- Some "file:"
    window.loadURL(url.format(opts))
    #else
    window.loadURL("http://localhost:9000/index.html")
    #endif

    // Emitted when the window is closed.
    window.on("closed", unbox(fun () ->
        // Dereference the window object, usually you would store windows
        // in an array if your app supports multi windows, this is the time
        // when you should delete the corresponding element.
        mainWindow <- Option.None
    )) |> ignore

    // Maximize the window
    window.maximize()

    #if RELEASE
    #else
    window.webContents.openDevTools()
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
