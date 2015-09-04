app.controller('companyJobsController', ['$scope', 'dataService', '$stateParams', '$rootScope', function ($scope, db, $stateParams, $rootScope) {

    $scope.title = "Jobs";
    $scope.navigate = function (page) {
        $scope.query = {
            keywords: $stateParams.q || $stateParams.keywords || '',
            location: $stateParams.location || '',
            skills: $stateParams.skills || '',
            startrate: $stateParams.startrate || '',
            endrate: $stateParams.endrate || '',
            xfrom: $stateParams.xfrom || '',
            xto: $stateParams.xto || '',
            industry: $stateParams.industry || ''
        }
        function fetchResults(query, page) {
            db.company.myJobs(page).success(function (result) {
                $scope.currentPage = page || 1;
                $scope.pages = Math.ceil(result.count / db.pageSize);
                angular.forEach(result.items, function (d) {
                    d.start = moment(d.start).toDate();
                    d.end = moment(d.end).toDate();
                });
                $scope.records = result.items;
            });
        }
        $scope.search = function (query) {
            console.log('query', query.skills)
            var q = '';
            for (var x in query) {
                q += (q === '' ? '?' : '&') + x + '=' + query[x];
            }
            window.location = '#/search' + q;
        }

        fetchResults($scope.query, page || 1);
    }
    $scope.navigate($stateParams.page);

    db.system.getSkills().success(function (result) {
        $scope.skills = result;
    });
    db.system.getLocations().success(function (result) {
        $scope.locations = result;
    });
    db.system.getIndustries().success(function (result) {
        $scope.industries = result;
    });

    $scope.resetFilters = function () {
        $scope.query.keywords = '';
        $scope.query.skills = '';
        $scope.query.location = '';
        $scope.query.startrate = '';
        $scope.query.endrate = '';
        $scope.query.xfrom = '';
        $scope.query.xto = '';
        $scope.query.industry = '';
        $scope.search(query);
    }

    //Slider configs
    $scope.experienceSlider = {
        min: $scope.query.xfrom || 0,
        max: $scope.query.xto || 500,
        ceil: 500,
        floor: 0
    };

    $scope.rateSlider = {
        min: $scope.query.startrate || 0,
        max: $scope.query.endrate || 500,
        ceil: 500,
        floor: 0
    };

    $scope.translate = function (value) {
        return '$' + value;
    }

    $scope.$on("slideEnded", function () {
        // user finished sliding a handle 
        console.log('slide ended')
        $scope.query.startrate = $scope.rateSlider.min;
        $scope.query.endrate = $scope.rateSlider.max;
        $scope.query.xfrom = $scope.experienceSlider.min;
        $scope.query.xto = $scope.experienceSlider.max;
        $scope.search($scope.query);

    });
}]);