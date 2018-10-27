var path = require("path");
var webpack = require("webpack");

function resolve(filePath) {
  return path.join(__dirname, filePath)
}

var babelOptions = {
  presets: [
    ["@babel/preset-env", {
      "targets": {
        "browsers": ["last 2 versions"]
      },
      "modules": false
    }]
  ]
};

var isProduction = process.argv.indexOf("-p") >= 0;
console.log("Bundling for " + (isProduction ? "production" : "development") + "...");

var basicConfig = {
  devtool: "source-map",
  node: {
    __dirname: false,
    __filename: false
  },
  module: {
    rules: [
      {
        test: /\.fs(x|proj)?$/,
        use: {
          loader: "fable-loader",
          options: {
            babel: babelOptions,
            define: isProduction ? [] : ["DEBUG"]
          }
        }
      },
      {
        test: /\.js$/,
        exclude: /node_modules/,
        use: {
          loader: 'babel-loader',
          options: babelOptions
        },
      },
      {
        test: /\.css$/,
        use: [
          { loader: "style-loader" },
          { loader: "css-loader" }
        ]
      }
    ]
  },
  mode: 'development',
  devServer: {
    hot: true,
    inline: true,
    port: 9000,
    contentBase: './app'
  },
  plugins: [
    new webpack.HotModuleReplacementPlugin()
  ]
};

// var mainConfig = Object.assign({
//   name: "mainConfig",
//   target: "electron-main",
//   entry: resolve("src/Main/Main.fsproj"),
//   output: {
//     path: resolve("app"),
//     filename: "main.js"
//   }
// }, basicConfig);

// if (!isProduction) {
//   basicConfig = Object.assign({
//     mode: 'development',
//     devServer: {
//       hot: true,
//       inline: true,
//       port: 9000,
//       contentBase: './app'
//     },
//     plugins: [
//       new webpack.HotModuleReplacementPlugin()
//     ]
//   }, basicConfig);
// }

var rendererConfig = Object.assign({
  name: "rendererConfig",
  target: "electron-renderer",
  entry: resolve("src/Renderer/Renderer.fsproj"),
  output: {
    path: resolve("app"),
    filename: "renderer.js"
  }
}, basicConfig);

module.exports = rendererConfig;