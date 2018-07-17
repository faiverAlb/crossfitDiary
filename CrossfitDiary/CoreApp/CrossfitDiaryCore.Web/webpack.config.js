const path = require('path');
const webpack = require('webpack');
const UglifyJsPlugin = require('uglifyjs-webpack-plugin');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

module.exports = (env) => {
  env = env || {};
  var isProd = env.NODE_ENV === 'production';

  // Setup base config for all environments
  var config = {
    entry: {
      main: './ClientApp/boot'
    },
    output: {
      path: path.join(__dirname, 'wwwroot/dist/generated'),
      filename: '[name].js'
    },
    devtool: 'eval-source-map',
    resolve: {
      extensions: ['.ts', '.tsx', '.js', '.jsx']
    },
    plugins: [
      new webpack.ProvidePlugin({ $: 'jquery', jQuery: 'jquery' }),
      new MiniCssExtractPlugin({
        // Options similar to the same options in webpackOptions.output
        // both options are optional
        filename: "[name].css",
        chunkFilename: "[id].css"
      })
    ],
    module: {
      rules: [
        {
          test: /\.(scss)$/,
          use: [
            isProd === false ? 'style-loader' : MiniCssExtractPlugin.loader,
            {
              loader: 'css-loader', // translates CSS into CommonJS modules
            }, {
              loader: 'postcss-loader', // Run post css actions
              options: {
                plugins: function() { // post css plugins, can be exported to postcss.config.js
                  return [
                    require('precss'),
                    require('autoprefixer')
                  ];
                }
              }
            }, {
              loader: 'sass-loader' // compiles Sass to CSS
            }
          ]
        } //        { test: /\.scss$/, use: [
//          "style-loader", // creates style nodes from JS strings
//          "css-loader", // translates CSS into CommonJS
//          "sass-loader" // compiles Sass to CSS
//        ] },
//        { test: /\.css?$/, use: ['style-loader', 'css-loader'] },
//        { test: /\.(png|jpg|jpeg|gif|svg)$/, use: 'url-loader?limit=25000' },
//        { test: /\.(png|woff|woff2|eot|ttf|svg)(\?|$)/, use: 'url-loader?limit=100000' }
      ]
    }
  }

  // Alter config for prod environment
  if (isProd) {
    config.devtool = 'source-map';
    config.plugins = config.plugins.concat([
      new UglifyJsPlugin({
        cache: true,
        parallel: true,
        uglifyOptions: {
          compress: true,
          ecma: 6,
          mangle: true
        },
        sourceMap: false
      })
    ]);
  }

  return config;

};