app.controller('companySearchController', ['$scope', 'dataService', '$routeParams', function ($scope, db, $routeParams) {
    $scope.title = "Contractor : Search Result";

    $scope.navigate = function (page) {
        $scope.query = {
            keywords: $routeParams.q || $routeParams.keywords || '',
            location: $routeParams.location || '',
            skills: $routeParams.skills || ''
        }

        function fetchResults(query, page) {
            db.company.search(query, page).success(function (result) {
                $scope.currentPage = page || 1;
                $scope.pages = Math.ceil(result.count / db.pageSize);
                $scope.count = result.count;
                $scope.records = result;
                $scope.page = page;
            });
        }

        $scope.search = function (query) {
            var q = '';
            for (var x in query) {
                q += (q === '' ? '?' : '&') + x + '=' + query[x];
            }
            window.location = '#/search' + q;
        }

        fetchResults($scope.query, page || 1);
    }
    $scope.navigate($routeParams.page);


}]);