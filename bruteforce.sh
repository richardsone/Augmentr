counter=0
echo "================Starting CURL Requests================"
while [ $counter -le 1000 ];
do
    echo "Making request #$counter"
    curl -d '{"Email":"fakeAF@mrstealurgurl.com", "Password":"lol", "name":"JimBob"}' -H "Content-Type: application/json" -v "localhost:12000/api/v1/auth/register"
    (( counter++ ))
    sleep 0.002
done
