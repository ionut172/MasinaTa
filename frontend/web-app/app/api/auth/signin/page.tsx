import Empty from "@/app/components/Empty";
import React from "react";

export default function page({searchParams}:{searchParams:{callbackUrl:string}}) {
    return(
        <Empty title="Trebuie sa fii logat" subtitle="Logheaza-te pentru a continua." showLogin callbackUrl={searchParams.callbackUrl}/>
    )
}