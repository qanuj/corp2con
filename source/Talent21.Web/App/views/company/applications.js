app.controller('companyApplicationsController', ['$scope', 'dataService', '$routeParams', function ($scope, db, $routeParams) {
    $scope.title = "Job Applications";
    $scope.folder = '';
    $scope.navigate = function (page) {
        var id = $routeParams.id;
        db.company.getJobApplications(id, page).success(function (result) {
            $scope.currentPage = page || 1;
            $scope.pages = Math.ceil(result.count / db.pageSize);
            $scope.records = result.items;
        });
    }
    $scope.navigate($routeParams.page);

    $scope.search = function (query) {
        console.log('query', query.skills)
        var q = '';
        for (var x in query) {
            q += (q === '' ? '?' : '&') + x + '=' + query[x];
        }
        window.location = '#/search' + q;
    }
}]);
