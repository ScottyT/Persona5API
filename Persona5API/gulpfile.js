/// <binding Clean='clean' />


var gulp = require("gulp"),
	sass = require("gulp-sass"),
	rimraf = require("rimraf"),
	concat = require("gulp-concat"),
	cssmin = require("gulp-cssmin"),
	merge = require("merge-stream"),
	terser = require("gulp-terser");

var paths = {
	webroot: "./wwwroot/"
};

var stylesDir = paths.webroot + "css/",
	vendorDir = paths.webroot + "lib/",
	scriptsDir = paths.webroot + "js/";

var cssFiles = [
	vendorDir + "bootstrap/dist/css/bootstrap.css"
];

gulp.task("scss", () => {
	var sassStream = gulp.src(stylesDir + "site.scss")
		.pipe(sass({ style: "expanded" }));
	var cssStream = gulp.src(cssFiles);
	return merge(cssStream, sassStream)
		.pipe(concat("site.min.css"))
		.pipe(gulp.dest(stylesDir));
});

var jsFiles = [
    vendorDir + "jquery/dist/jquery.js",
    vendorDir + "jqueryui/jquery-ui.min.js",
	vendorDir + "jquery-validation/dist/jquery.validate.js",
	vendorDir + "jquery-validation-unobtrusive/jquery.validate.unobtrusive.js",
	vendorDir + "vue/vue.min.js",
	vendorDir + "axios/axios.min.js",
	vendorDir + "bootstrap/dist/js/bootstrap.bundle.js",
	vendorDir + "bootstrap-select/js/bootstrap-select.js",
	scriptsDir + "persona.main.js"
];

gulp.task("js", function() {
	return gulp.src(jsFiles)
		.pipe(concat("site.min.js"))
		.pipe(terser())
		.pipe(gulp.dest(scriptsDir));
});

gulp.task("watch", () => {
	gulp.watch([scriptsDir + "*.js", "!" + scriptsDir + "site.min.js"], ["js"]);
	gulp.watch(stylesDir + "*.scss", ["scss"]);
});

gulp.task('default', gulp.series["scss", "js"]);