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
  }
};

if (!isProduction) {
  basicConfig = Object.assign({
    mode: 'development',
    watch: true
  }, basicConfig);
}

var mainConfig = Object.assign({
  name: "mainConfig",
  target: "electron-main",
  entry: resolve("src/Main/Main.fsproj"),
  output: {
    path: resolve("app"),
    filename: "main.js"
  }
}, basicConfig);

module.exports = mainConfig;