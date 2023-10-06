import React from "react";
type Props = {
    title: string
    subtitle: string
    center? : boolean
}

export default function Header({title, subtitle, center} : Props) {
    return (
        <div className={center ? 'text-center' : 'text-start'}>
            <div className="text-2xl font-bold">
                {title}
            </div>
            <div className="font-light text-netural-500 mt-2">
                {subtitle}
            </div>
        </div>
    )
}