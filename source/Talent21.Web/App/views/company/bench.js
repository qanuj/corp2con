app.controller('companyBenchController', ['$scope', 'dataService', '$routeParams', function ($scope, db, $routeParams) {
    $scope.title = "Bench Management";
    $scope.navigate = function (page) {
        db.company.benches(page).success(function (result) {
            $scope.currentPage = page || 1;
            $scope.pages = Math.ceil(result.count / db.pageSize);
            $scope.records = result.items;
        });
    }

    $scope.invite=function(people) {
        if(!people.length) return;
        db.company.invite(people).success($scope.navigate);
    }

    $scope.navigate($routeParams.page);
}]);

