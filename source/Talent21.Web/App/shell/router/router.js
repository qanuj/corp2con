app.config(function($routeProvider, $locationProvider) {
    $routeProvider
        .when('/', {
            templateUrl: '/app/shell/views/home.html',
            controller: 'homeController'
        })
        .when('/others', {
            templateUrl: '/app/shell/views/candidate/other.html',
            controller: 'otherController'
        })
          .when('/candidateprofile', {
              templateUrl: '/app/shell/views/candidate/candidateProfile.html',
              controller: 'candidateProfileController'
          })
    ;
    $locationProvider.html5Mode(false).hashPrefix('');
});