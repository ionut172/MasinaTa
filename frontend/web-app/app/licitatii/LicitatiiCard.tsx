
import React from "react";
import CountdownTimer from "./CountdownTimer";
import LicitatiiImage from "./LicitatiiImage";
import { Licitatii } from "@/types";
import Link from "next/link";
import CurrentBid from "./CurrentBid";
type Props = {
    licitatie: Licitatii,

}
export default function LicitatiiCard({licitatie}: Props) {
   

    return (
      <Link href={`/licitatii/details/${licitatie.id}`} className="group">
        <div className="w-full bg-gray-200 aspect-w-16 aspect-h-10 rounded-lg overflow-hidden">
            <div>
            <LicitatiiImage imagineUrl={licitatie.imagineUrl}/>
            <div className="absolute bottom-2 left-2">
            <CountdownTimer licitatieEnd={licitatie.licitatieEnd}/>
            </div>
            <div className="absolute top-2 right-2">
            <CurrentBid pretRezervare={licitatie.pretRezervare} celMaiMareBid={licitatie.celMaiMareBid}/>
            </div>
            </div>
           
        </div>
        <div className=" flex justify-between items-center mt-4">
                <h3 className="text-gray-700">{licitatie.make} {licitatie.modelMasina}</h3>
                <p className="font-semibold text-sm">{licitatie.an}</p>
           </div>
          
      </Link>
    );
}