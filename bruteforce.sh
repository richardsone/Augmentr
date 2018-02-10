counter=0
while [ $counter -le 15 ];
do
    curl -v -O localhost:12000/api/v1/auth/login  # This can be changed to whatever, needs some params anyways
    (( counter++ ))
    sleep 2
done
