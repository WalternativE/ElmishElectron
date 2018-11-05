const path = require("path");
const webpack = require("webpack");

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
  mode: isProduction ? 'production' : 'development',
  devtool: "source-map",
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
        test: /\.scss|.css$/,
        use: [
          { loader: "style-loader" },
          { loader: "css-loader" },
          { loader: "sass-loader" }
        ]
      },
      {
        test: /\.(woff(2)?|ttf|eot|svg)(\?v=\d+\.\d+\.\d+)?$/,
        use: [{
          loader: 'file-loader',
          options: {
            name: '[name].[ext]',
            outputPath: 'assets/'
          }
        }]
      }
    ]
  }
};

var mainConfig = Object.assign({
  watch: isProduction ? false : true,
  node: {
    "__dirname": true
  },
  name: "mainConfig",
  target: "electron-main",
  entry: resolve("src/Main/Main.fsproj"),
  output: {
    path: resolve("app"),
    filename: "main.js"
  }
}, basicConfig);

const hmrPlugin = new webpack.HotModuleReplacementPlugin();

var rendererConfig = Object.assign({
  name: "rendererConfig",
  target: "electron-renderer",
  entry: resolve("src/Renderer/Renderer.fsproj"),
  output: {
    path: resolve("app"),
    filename: "renderer.js"
  },
  plugins: isProduction ? [] : [hmrPlugin]
}, basicConfig);

if (!isProduction) {
  rendererConfig = Object.assign({
    devServer: {
      hot: true,
      inline: true,
      port: 9000,
      contentBase: resolve("app")
    }
  }, rendererConfig);
}

module.exports = [mainConfig, rendererConfig];