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
        .when('/companyprofile', {
            templateUrl: '/app/shell/views/company/companyprofile.html',
            controller: 'companyprofileController'
        })

        .when('/joblisting', {
            templateUrl: '/app/shell/views/job/jobListing.html',
            controller: 'jobListingController'
        })
        .when('/aboutcompany', {
            templateUrl: '/app/shell/views/company/aboutcompany.html',
            controller: 'aboutCompanyController'
        });
    $locationProvider.html5Mode(false).hashPrefix('');
});