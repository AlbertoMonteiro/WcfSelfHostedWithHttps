call "%vs140comntools%/vsdevcmd"
mkdir certificados
cd certificados
makecert.exe -sk RootCA -sky signature -pe -n CN=localhost -r -sr LocalMachine -ss Root MyCA.cer
makecert.exe -sk server -sky exchange -pe -n CN=localhost -ir LocalMachine -is Root -ic MyCA.cer -sr LocalMachine -ss My MyAdHocTestCert.cer

cd ..

msbuild eSocial.sln /v:m

cd src\CertificateBinderToPort\bin\Debug

CertificateBinderToPort.exe