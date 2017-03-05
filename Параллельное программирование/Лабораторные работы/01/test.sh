file=$1

start=$2
startlength=$(expr length ${start##*.})

end=$3
endlength=$(expr length ${end##*.})

threads=(2 4 8)
[[ $startlength -gt $endlength ]] && scale="$startlength" || scale="$endlength"

rm -rf result.txt

while [[ $(echo "$start < $end" | bc -l) != 1 ]]; do

    echo -e "for $start:\ntime:" >> result.txt

    first=$(./program 10 90 $start 1)
    echo -e "\t$first" >> result.txt
    k=(1)

    for i in ${threads[@]}; do
        current=$(./program 10 90 $start $i)
        k+=("$(echo "scale=$scale; $first/$current" | bc)")
        echo -e "\t$current" >> result.txt
    done

    echo "k:" >> result.txt
    for i in ${k[@]}; do
        echo -e "\t$i" >> result.txt
    done

    echo -e "\n\n" >> result.txt

    echo "$start done!";
    start=$(echo "scale=$scale; $start/10" | bc)
done