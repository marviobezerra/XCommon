/// <binding ProjectOpened='dev:watch' />
var gulp = require('gulp'),
	gutil = require('gulp-util'),
	webpack = require('webpack'),
	htmlmin = require('gulp-htmlmin'),
	shell = require('gulp-shell'),
	livereload = require("gulp-livereload"),
	rimraf = require("gulp-rimraf"),
	webpackStream = require("webpack-stream"),
	merge = require('webpack-merge');

var helper = {
	tasks: {
		clear: "clear",
		watch: "dev:watch",
		html: {
			page: "dev:html:page"
		},
		default: {
			dev: "default:dev",
			deploy: "default:deploy"
		}
	},
	path: {
		source: {
			defaultFile: "./App/App.Component.ts",
			html: "./App/**/*.html"
		},
		destination: {
			html: "./wwwroot/html",
			assets: "./wwwroot/assets"
		}
	},
	htmlMimify: {
		collapseWhitespace: true,
		removeComments: true,
		removeTagWhitespace: false,
		removeRedundantAttributes: true,
		caseSensitive: true
	},
	webpack: function (dev, watch, deploy) {
		var result = merge(require('./webpack.config.js'), {
			watch: watch,
			devtool: dev ? "source-map" : ""
		});

		if (watch === true) {
			result.plugins = result.plugins || [];
			result.plugins.push(helper.webPackLog);
		}

		if (deploy === true) {
			result.plugins = result.plugins || [];
			result.plugins.push(new webpack.optimize.UglifyJsPlugin({
				minimize: true
			}));
		}

		return result;
	},
	webPackLog: function () {
		this.plugin("done", function (stats) {
			if (stats.compilation.errors && stats.compilation.errors.length) {
				console.log("");
				console.log("********************************************************************************");
				console.log("********************************   ERROR   *************************************");
				console.log("");
				console.log(stats.compilation.errors);
				console.log("********************************************************************************");
				console.log("");
				stats.compilation.errors = [];
			}
		});
	}
};

gulp.task(helper.tasks.clear, function (cb) {
	return gulp.src([helper.path.destination.assets, helper.path.destination.html], { read: false })
		.pipe(rimraf());
})

gulp.task(helper.tasks.html.page, function () {
	gulp.src([helper.path.source.html])
		.pipe(htmlmin(helper.htmlMimify))
		.pipe(gulp.dest(helper.path.destination.html))
		.pipe(livereload());
});

gulp.task(helper.tasks.default.dev, [helper.tasks.html.page], function () {
	return gulp.src(helper.path.source.defaultFile)
		.pipe(webpackStream(helper.webpack(true, false, false)))
		.pipe(gulp.dest(helper.path.destination.assets));
});

gulp.task(helper.tasks.default.deploy, [helper.tasks.html.page], function () {
	return gulp.src(helper.path.source.defaultFile)
		.pipe(webpackStream(helper.webpack(false, false, true)))
		.pipe(gulp.dest(helper.path.destination.assets));
});

gulp.task(helper.tasks.watch, [helper.tasks.html.page], function () {
	livereload({ start: true });

	gulp.watch([helper.path.source.html], [helper.tasks.html.page]);

	return gulp.src(helper.path.source.defaultFile)
		.pipe(webpackStream(helper.webpack(true, true, false)))
		.pipe(gulp.dest(helper.path.destination.assets))
		.pipe(livereload());
});