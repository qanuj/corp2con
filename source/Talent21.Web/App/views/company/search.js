app.controller('companySearchController', ['$scope', 'dataService', '$routeParams', function ($scope, db, $routeParams) {
    $scope.title = "Contractor : Search Result";

    console.log($routeParams);

    $scope.query = {
        keywords: $routeParams.q || $routeParams.keywords || '',
        location: $routeParams.location || '',
        skills: $routeParams.skills || ''
    }

    function fetchResults(query, page) {
        db.company.search(query, page).success(function (result) {
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
        window.location = '#/search' + q;
    }

    fetchResults($scope.query, $routeParams.page || 1);

}]);