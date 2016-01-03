var gulp  = require("gulp");
var watch = require("gulp-watch");
var exec  = require("child_process").exec;

function build() {
	exec('build', {}, function(err, stdout, stderr) {
		console.log(new Array(61).join("-"));
		console.log(stdout);
		console.log(new Array(61).join("-"));
	});
}

gulp.task("build", function() {
	build();
});

gulp.task("incbuild", function() {
	watch("*.cs", { events: ["change"] }, build);
});

