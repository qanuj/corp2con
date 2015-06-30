app.controller('contractorEditProfileController', ['$scope', 'dataService', function ($scope, db) {

    db.contractor.profile().success(function(result) {
        $scope.record = result;
        $scope.years = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10'];
        $scope.months = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'];
    });

    $scope.save = function(record) {
        db.contractor.editProfile(record).success(function (result) {
            console.log(result);
            if (!result.success) {
                console.log('Error');
            }
        });
    }
}]);