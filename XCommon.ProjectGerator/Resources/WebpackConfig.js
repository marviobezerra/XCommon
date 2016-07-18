var path = require('path'),
	ExtractTextPlugin = require("extract-text-webpack-plugin"),
	webpack = require('webpack');

module.exports = {
	entry: [
		"./App/Polyfills.ts",
		"./App/Vendor.ts",
		"./App/App.Component.ts",
        "./App/Styles/App.Theme.scss"
	],
	output: {
		path: path.join(__dirname, "wwwroot", "asserts"),
		filename: "bundle.js"
	},
	plugins: [
		new ExtractTextPlugin("bundle.css")
	],

	resolve: {
		extensions: ["", ".js", ".ts", ".scss", ".css"]
	},

	module: {
		loaders: [
			{
				test: /\.css$/,
				exclude: /node_modules/,
				loader: ExtractTextPlugin.extract("style-loader", "css-loader")
			},
            {
                test: /\App.Theme.scss$/,
                exclude: /node_modules/,
                loader: ExtractTextPlugin.extract("style-loader", "css-loader!sass-loader")
            },
			{
			    exclude: /Styles/,
			    test: /\.scss$/,
			    loaders: ['raw-loader', 'sass-loader?sourceMap']
			},
			{
				test: /\.ts$/,
				loader: "ts-loader"
			},
			{
				test: /\.js$/,
				loader: 'strip-sourcemap'
			}
		],
		noParse: [/angular2\/bundles\/.+/]
	}
};