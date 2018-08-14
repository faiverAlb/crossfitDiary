const path = require('path');
const webpack = require('webpack');
const UglifyJsPlugin = require('uglifyjs-webpack-plugin');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const OptimizeCSSAssetsPlugin = require('optimize-css-assets-webpack-plugin');

const destinationFolder = 'wwwroot/dist/generated';
module.exports = (env) => {
  env = env || {};
  var isProd = env.NODE_ENV === 'production';

  // Setup base config for all environments
  var config = {
    entry: {
      main: './ClientApp/boot.ts',
      login: './ClientApp/login-page-entry',
      fontAwesome:'@fortawesome/fontawesome-free/js/all.js'
    },
    output: {
      path: path.join(__dirname, destinationFolder),
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
      }),
    ],
    module: {
      rules: [
        {
          test: /\.(scss)$/,
          use: [
            MiniCssExtractPlugin.loader,
//            isProd === false ? 'style-loader' : MiniCssExtractPlugin.loader,
            {
              loader: 'css-loader',
            },
            {
              loader: 'postcss-loader', // Run post css actions
              options: {
                plugins: function() { // post css plugins, can be exported to postcss.config.js
                  return [
                    require('precss'),
                    require('autoprefixer')
                  ];
                }
              }
            }, 
            {
              loader: 'sass-loader'
            }

          ]
        },
        // the url-loader uses DataUrls. 
        // the file-loader emits files. 
        {
          test: /\.woff(2)?(\?v=[0-9]\.[0-9]\.[0-9])?$/,
          loader: "url-loader?limit=10000&mimetype=application/font-woff"
        },
        {
          test: /\.(ttf|eot|svg)(\?v=[0-9]\.[0-9]\.[0-9])?$/,
          loader: "file-loader"
        },
        {
          test: /\.vue$/,
          loader: 'vue-loader'
        },
        {
          test: /\.ts$/,
          loader: 'ts-loader',
          exclude: /node_modules/,
          options: {
            appendTsSuffixTo: [/\.vue$/]
          }
        }
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
      }),
      new OptimizeCSSAssetsPlugin({})
    ]);
  }

  return config;

};