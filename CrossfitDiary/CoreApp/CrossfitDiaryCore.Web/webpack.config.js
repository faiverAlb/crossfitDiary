const path = require('path');
const webpack = require('webpack');
const UglifyJsPlugin = require('uglifyjs-webpack-plugin');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const OptimizeCSSAssetsPlugin = require('optimize-css-assets-webpack-plugin');
const VueLoaderPlugin = require('vue-loader/lib/plugin')
const destinationFolder = 'wwwroot/dist/generated/';
const CleanWebpackPlugin = require('clean-webpack-plugin')
// const SpeedMeasurePlugin = require("speed-measure-webpack-plugin");
// const smp = new SpeedMeasurePlugin();

// the path(s) that should be cleaned
let pathsToClean = [destinationFolder + '*.*']

// the clean options to use
let cleanOptions = {
  verbose: true,
  dry: false
};

module.exports = (env) => {
  env = env || {};
  var isProd = env.NODE_ENV === 'production';

  // Setup base config for all environments
  var config = ({
    entry: {
      'persons-entry': './ClientApp/persons-entry.ts',
      'login': './ClientApp/login-page-entry',
      'workout-entry': './ClientApp/workout-entry.ts',
    },
    output: {
      path: path.join(__dirname, destinationFolder),
      filename: '[name].js',
      pathinfo: false
    },
    devtool: 'eval-source-map',
    resolve: {
      extensions: ['.ts', '.tsx', '.js', '.jsx', '.vue'],
      alias: {
        'vue$': 'vue/dist/vue.esm.js',
        jquery: "jquery/src/jquery",
      }
    },
    plugins: [
      new CleanWebpackPlugin(pathsToClean, cleanOptions),
      new webpack.ProvidePlugin({
        Vue: ['vue/dist/vue.esm.js', 'default'],
        jQuery: 'jquery',
        'window.jQuery': 'jquery',
        $: 'jquery',
        moment: 'moment',
      }),
      new VueLoaderPlugin(),
      new MiniCssExtractPlugin({
        // Options similar to the same options in webpackOptions.output
        // both options are optional
        filename: "[name].css",
        chunkFilename: "[id].css"
      })

    ],
    module: {
      rules: [{
          test: /\.(scss)$/,
          use: [
            MiniCssExtractPlugin.loader,
            //            isProd === false ? 'style-loader' : MiniCssExtractPlugin.loader,
            {
              loader: 'css-loader',
            },
            {
              loader: 'sass-loader'
            }

          ]
        },
        {
          test: /\.css?$/,
          loaders: ['style-loader', 'css-loader', 'sass-loader']
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
          loader: 'vue-loader',
          options: {
            loaders: {
              'scss': 'vue-style-loader!css-loader!sass-loader',
              'sass': 'vue-style-loader!css-loader!sass-loader?indentedSyntax'
            }
          }
        },
        {
          test: /\.ts$/,
          loader: 'ts-loader',
          exclude: /node_modules/,
          options: {
            appendTsSuffixTo: [/\.vue$/],
            transpileOnly: !isProd,
            experimentalWatchApi: !isProd,
          }
        }
      ]
    },
    optimization: {
      splitChunks: {
        chunks: 'async',
        minSize: 30000,
        maxSize: 0,
        minChunks: 1,
        maxAsyncRequests: 5,
        maxInitialRequests: 3,
        automaticNameDelimiter: '~',
        name: true,
        cacheGroups: {
          vendors: {
            test: /[\\/]node_modules[\\/]/,
            priority: -10
          },
          default: {
            minChunks: 2,
            priority: -20,
            reuseExistingChunk: true
          }
        }
      }
    }
  })

  // Alter config for prod environment
  if (isProd) {
    // config.devtool = 'source-map'; //It causes issues
    config.plugins = config.plugins.concat([
      new UglifyJsPlugin({
        cache: true,
        parallel: true,
        uglifyOptions: {
          compress: true,
          // ecma: 6,
          mangle: true,
          dead_code: true
        },
        sourceMap: false,
      }),
      // new OptimizeCSSAssetsPlugin({
      //   cssProcessorOptions: {
      //     discardComments: {
      //       removeAll: true
      //     },
      //   },
      // })
    ]);
  }

  if (env.ENTRY) {
    var temp = config.entry[env.ENTRY];
    config.entry = {};
    config.entry[env.ENTRY] = temp;
  }

  return config;

};