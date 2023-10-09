'use client'
import React from "react";
type Props = {
    celMaiMareBid: number,
    pretRezervare: number, 
}

export default function CurrentBid({celMaiMareBid, pretRezervare}:Props) {
    const text = celMaiMareBid ? '$' +  celMaiMareBid: 'Fara licitatii inca';
    const culoare = celMaiMareBid ? celMaiMareBid > pretRezervare ? 'bg-green-600' : 'bg-amber-600' : 'bg-red-600';
    return(
        <div className={`
            border-2 border-white text-white py-1 px-2 rounded-lg flex
            justify-center ${culoare}
        `}>
            {text}
        </div>

    )
}