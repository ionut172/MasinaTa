'use client'
import React from "react";
import Countdown, { zeroPad } from "react-countdown";

type Props = {
    licitatieEnd: string;
}

const renderer = ({days, hours, minutes, seconds, completed}:{days:number, hours:number, minutes: number, seconds: number, completed: boolean}) => {
    return (
        <div className={`
        border-2 border-white text-white py-1 px-2 rounded-lg flex justify-center
        ${completed ? 'bg-red-600' : (days === 0 && hours < 10) ? 'bg-amber-600' : 'bg-green-400'}
        `}>
                {completed ? (
                    <span> Licitatie terminata</span>
                ) : (
                    <span suppressHydrationWarning={true}> {zeroPad(days)}:{zeroPad(hours)}:{zeroPad(minutes)}:{zeroPad(seconds)}</span>
                )}
        </div>
    )
}
export default function CountdownTimer({licitatieEnd}:Props){
    return (
        <div>
        <Countdown date={licitatieEnd} renderer={renderer}/>
      
        </div>
    )
}