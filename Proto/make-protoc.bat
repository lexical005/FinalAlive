@cls
@echo off

MsgGen.py proto3 proto3

echo Server
protoc-3.3.0.exe --go_out=. Server/ffProto.proto

cd Server

go fmt ffProto.pb.go
go fmt message.pb.go

if exist ..\..\..\FinalAliveServer\ffServer\src\ffProto (
	echo copy ffProto.pb.go
	copy ffProto.pb.go ..\..\..\FinalAliveServer\ffServer\src\ffProto\ /y
	
	echo copy message.pb.go
	copy message.pb.go ..\..\..\FinalAliveServer\ffServer\src\ffProto\ /y
	
	echo go install ffProto
	go install ffProto
)
cd ..


echo;
echo Client
protoc-3.0.2.exe --csharp_out=./Client/ Client/Proto.proto


echo;
pause
