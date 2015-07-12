app.controller('contractorSearchController', ['$scope', 'dataService', '$routeParams', function ($scope, db, $routeParams) {
    $scope.title = "Jobs : Search Result";
   
    $scope.query= {
        keywords: $routeParams.q || $routeParams.keywords,
        location: $routeParams.location||'',
        skills: $routeParams.skills || ''
    }

    function fetchResults(query,page) {
        db.contractor.searchJob(query, page).success(function (result) {
            $scope.records = result;
            console.log(result)
        });
    }

    $scope.search = function (query) {
        var q = '';
        for (var x in query) {
            q += (q==='' ? '?' : '&') + x + '=' + query[x];
        }
        window.location = '#/search'+q;
    }

    fetchResults($scope.query, $routeParams.page || 1);
}]);