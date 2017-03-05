file=$1
start=$2
end=$3
eps=$4 

threads=(2 4 8)

scale=2

rm -rf result.txt

let preend=start*2

while [[ $(echo "$preend > $end" | bc -l) != 1 ]]; do

    echo -e "for $start..$preend:\ntime:" >> result.txt

    first=$($file $start $preend $eps 1)
    fvalues=($first)
    echo -e "\t${fvalues[0]} it. -- ${fvalues[1]} s" >> result.txt
    k=(1)

    for i in ${threads[@]}; do
        current=$($file $start $preend $eps $i)
        values=($current)
        k+=("$(echo "scale=$scale; ${fvalues[1]}/${values[1]}" | bc)")
        echo -e "\t${values[0]} it. -- ${values[1]} s" >> result.txt
    done

    echo "k:" >> result.txt
    for i in ${k[@]}; do
        echo -e "\t$i" >> result.txt
    done

    echo -e "\n\n" >> result.txt

    echo "$preend done!";

    let preend=preend*3/2
done

echo "all done!"