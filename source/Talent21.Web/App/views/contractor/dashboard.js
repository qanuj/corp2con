app.controller('contractorDashboardController', ['$scope', 'dataService' , '$routeParams', function ($scope, db, $routeParams) {
    $scope.getApplications = function () {
        db.contractor.getJobApplications().success(function (result) {
            angular.forEach(result.items, function (item) {
                db.applied.push(item.job.id);
            });
            console.log(array);
        });
    }

    $scope.getApplications();
    db.contractor.dashboard().success(function (result) {
        $scope.record = result;
        
    });

    db.contractor.get().success(function (result) {
        $scope.profile = result;
    });
}]);