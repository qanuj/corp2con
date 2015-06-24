/// <reference path="../views/home.html" />
app.config(function($routeProvider, $locationProvider) {
    $routeProvider
        .when('/', {
            templateUrl: '../views/home.html',
            controller: '../controllers/homeController.js'
        })
        .when('/other', {
            templateUrl: '~/App/shell/views/home.html',
            controller: '../controllers/headerController.js'
        });
    $locationProvider.html5Mode(true);
});