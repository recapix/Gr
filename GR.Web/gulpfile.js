/// <binding BeforeBuild='styles' />
"use strict";

/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. https://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp'),
    sass = require('gulp-sass'),
    sourcemaps = require('gulp-sourcemaps');

//style paths
var sassFiles = 'Client-src/scss/Site.scss',
    cssDest = 'wwwroot/css/';

gulp.task('styles', function () {
    gulp.src(sassFiles)
        .pipe(sourcemaps.init())
        .pipe(sass().on('error', sass.logError))
        .pipe(sourcemaps.write())
        .pipe(gulp.dest(cssDest));
});
