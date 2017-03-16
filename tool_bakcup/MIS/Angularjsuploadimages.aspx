<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Angularjsuploadimages.aspx.cs" Inherits="Angularjsuploadimages" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head data-ng-app="UploadApp">
    <title>AngularJS Upload Images using File Upload Plugin</title>
    <script src="//ajax.googleapis.com/ajax/libs/angularjs/1.3.9/angular.min.js"></script>
    <script src="scripts/ng-file-upload-shim.min.js" type="text/javascript"></script>
    <script src="scripts/ng-file-upload.min.js" type="text/javascript"></script>
    <script type="text/javascript">
    //inject angular file upload directives and services.
var app = angular.module('fileUpload', ['ngFileUpload']);

app.controller('MyCtrl', ['$scope', 'Upload', '$timeout', function ($scope, Upload, $timeout) {
    $scope.uploadPic = function(file) {
    file.upload = Upload.upload({
      url: 'UploadHandler.ashx',
      data: {file: file}
    });

    file.upload.then(function (response) {
      $timeout(function () {
        file.result = response.data;
      });
    }, function (response) {
      if (response.status > 0)
        $scope.errorMsg = response.status + ': ' + response.data;
    }, function (evt) {
      // Math.min is to fix IE which reports 200% sometimes
      file.progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
    });
    }
}]);
    </script>
     <style type="text/css">
form .progress {
    line-height: 15px;
}
.progress {
    display: inline-block;
    width: 100px;
    border: 3px groove #CCC;
}
.progress div {
    font-size: smaller;
    background: orange;
    width: 0;
}
     </style>
</head>
<body ng-app="fileUpload" ng-controller="MyCtrl">
  <form name="myForm">
      <h3>AngularJS Upload Multiple Files</h3>
     Upload:
      <input type="file" ngf-select ng-model="picFile" name="file" multiple accept="*" ngf-max-size="10MB" required
             ngf-model-invalid="errorFile">
      <i ng-show="myForm.file.$error.required">*required</i><br>
      <i ng-show="myForm.file.$error.maxSize">File too large 
          {{errorFile.size / 1000000|number:1}}MB: max 10MB</i>
      <br>
      <button ng-disabled="!myForm.$valid" ng-click="uploadPic(picFile)">Submit</button><br />
      <span class="progress" ng-show="picFile.progress >= 0">
        <div style="width:{{picFile.progress}}%" 
            ng-bind="picFile.progress + '%'"></div>
      </span>
      <span ng-show="picFile.result">Upload Successful</span>
      <span class="err" ng-show="errorMsg">{{errorMsg}}</span>
  </form>
</body>
</html>
