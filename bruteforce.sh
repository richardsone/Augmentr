counter=0
echo "Starting CURL Requests"
while [ $counter -le 15 ];
do
    echo "Making request #$counter"
    curl -d {"email":"fakeAF@mrstealurgurl.com", "password":"lol$counter$counter$counter", "name":"JimBob"} -v -O https://localhost:12000/api/v1/auth/register  # This can be changed to whatever, needs some params anyways
    (( counter++ ))
    sleep 0.002
done