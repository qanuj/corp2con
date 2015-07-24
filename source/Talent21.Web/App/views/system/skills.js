app.controller('jobskillsController', ['$scope', 'dataService', function ($scope, db) {

    //$scope.save = function (record) {
    //    record.skill = [];
    //    db.system.addSkill(record).success(function (refreshRecord) {
    //        console.log(refreshRecord);
    //    });
    //}

    $scope.save = function (record) {
        db.system.addSkill(record).success(refreshRecord);
        console.log(refreshRecord);
    }
    
    function refreshRecord() {
        return db.system.getSkills().success(function (result) {
            $scope.skill = result;
        });
    }
   
    $scope.update = function (s) {
        db.system.editSkill(s).success(refreshRecord);
    };
   
    $scope.delete = function (s) {
        db.system.deleteSkill(s).success(result);
    };

    $scope.toggle = function (s) {
        s.editMode = !s.editMode;
    };
    refreshRecord();
}]);

