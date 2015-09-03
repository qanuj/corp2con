app.controller('companyInviteController', ['$scope', 'dataService', '$stateParams', function($scope, db, $stateParams) {
    $scope.title = "Invite Bench Team";
    $scope.save = "Sent Invitations";
    $scope.records = [];

    function addRecords(x) {
        for (var i = 0; i < x; i++) {
            $scope.records.push({ name: '', email: '' });
        }
    }

    $scope.add = addRecords;

    $scope.sendInvites = function (rows) {
        var invs = [];
        for (var x in rows) {
            if (!!rows[x].name && !!rows[x].email) invs.push(rows[x]);
        }
        $scope.status = "sending";
        db.company.sendInvites(invs).success(function () {
            $scope.sent = "sent";
        });
    }

    addRecords(10);

}]);