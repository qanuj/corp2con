app.controller('contractorApplicationsController', ['$scope', 'dataService', '$routeParams', function ($scope, db, $routeParams) {
    $scope.title = "Job Applications";
    var array = [];
    $scope.getApplications = function () {
        db.contractor.getJobApplications().success(function (result) {
            console.log(result.items);
            $scope.records = result.items;
            var item = new String('Item ' + c)
            var c = $scope.records.length + 1;
            angular.forEach(result.items, function (item) {
                array.push(item.job.id);
            });
            console.log(array);
        });
    }

    $scope.getApplications();

}]);