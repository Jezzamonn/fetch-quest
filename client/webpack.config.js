const path = require('path');
const webpack = require('webpack');
const nodeExternals = require('webpack-node-externals');

const common = {
    module: {
        rules: [
            {
                test: /\.js$/,
                exclude: /node_modules/,
                loader: 'babel-loader',
            }
        ]
    },
    stats: {
        colors: true
    },
}

const client = {
    entry: './js/main.js',
    output: {
        path: path.resolve(__dirname, 'build/js'),
        filename: 'main.bundle.js'
    },
    mode: 'development',
    devtool: 'source-map'
}

module.exports = [
    Object.assign({}, common, client),
];