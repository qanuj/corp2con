app.controller('contractorApplicationsController', ['$scope', 'dataService', '$routeParams', function ($scope, db, $routeParams) {
    $scope.title = "Applied Jobs";

    $scope.query = {
        keywords: $routeParams.q || $routeParams.keywords || '',
        location: $routeParams.location || '',
        skills: $routeParams.skills || ''
    }

    console.log($routeParams, $scope.query);


    function fetchResults(query, page) {
        db.contractor.search(query, page).success(function (result) {
            $scope.count = result.count;
            $scope.records = result.items;
            $scope.page = page;
            $scope.pages = db.findPages(result);
        });
    }

    $scope.search = function (query) {
        var q = '';
        for (var x in query) {
            q += (q === '' ? '?' : '&') + x + '=' + query[x];
        }
        window.location = '#/applications' + q;
    }

    fetchResults($scope.query, $routeParams.page || 1);
}]);