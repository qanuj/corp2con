
app.controller('jobskillsController', ['$scope', 'dataService', function ($scope, db) {

    $scope.save = function (record) {
        //record.skills = [];
        db.system.addSkill(record).success(function (result) {
            console.log(result);
        });
    }



    $scope.update = function (s) {
        db.system.editSkill(s).success(result);
    };

    $scope.delete = function (s) {
        db.system.deleteSkill(s).success(result);
    };

    $scope.toggle = function (s) {
        s.editMode = !s.editMode;
    };

}]);














