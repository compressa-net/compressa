# Compressa
Compressa is an book reader application for busy people

## AI services that were explored
- [x] Stable Diffusion - logo
- [x] AssemblyAI - audio to text, chapter summary
- [x] co:here - text compression (!! generated low quality summaries, I can't use them.)
- [x] wordtune - text compression (!! they don't have a public API, I can't use them.)
- [ ] ChatGPT - text compression
- [ ] Uberduck - text-to-speech
- [x] Microsoft Speech Studio - customized text-to-speech (!! seems like a good option for custom voices, but needs special permission from Microsoft)
- [ ] Google Translate - translation
- [x] GitHub Copilot - programming

## Steps to completion
- [x] Start a new solution the basic templates of .NET 7 ASP.NET and MAUI.
- [x] Use FFMPEG to convert audible audiobooks to m4b and mp3 chapter files
- [ ] Use the AssemblyAI service to transcribe the chapters, and test their summarization method
- [ ] Use ChatGPT to generate x4 and x16 compressed text
- [ ] Create a basic GUI
- [ ] List available audiobooks
- [ ] Show the summaries for at least one book
- [ ] Make the UI capable to change zoom levels amoung 4 levels (list of books, chapters + x64 summary, x16 summary and x4 summary)
- [ ] Make a 2 minute video
- [ ] Submit the project on Devpost

## Text summary tests

The target is to have multiple levels of compression.
(a) a one-liner for the whole chapter
(b) a ~100 word summary of a chapter, approximately a 64x compression
(c) a x16 compression of bullet points
(d) a x4 compression for the sections

### wordtune

#### Input text (1248 words, 7876 characters)
```
Artificial General Intelligence Artificial intelligence (AI) is a branch of computer science that aims to design machines that can mimic or replicate human intelligence. Because of the success of machine learning as a paradigm, we’ve made enormous progress in AI over the last ten years. Machine learning is a method of creating useful algorithms that does not require explicitly programming them; instead, it relies on learning from data, such as images, the results of computer games, or patterns of mouse clicks. One well-publicised breakthrough was DeepMind’s AlphaGo in 2016, which beat eighteen-time international champion Go player Lee Sedol.36 But AlphaGo is just a tiny sliver of all the impressive achievements that have come out of recent developments in machine learning. There have also been breakthroughs in generating and recognising speech, images, art, and music; in real-time strategy games like StarCraft; and in a wide variety of tasks associated with understanding and generating humanlike text.37 You probably use artificial intelligence every day, for example in a Google search.38 AI has also driven significant improvements in voice recognition, email text completion, and machine translation.39 The ultimate achievement of AI research would be to create artificial general intelligence, or AGI: a single system, or collection of systems working together, that is capable of learning as wide an array of tasks as human beings can and performing them to at least the same level as human beings.40 Once we develop AGI, we will have created artificial agents—beings (not necessarily conscious) that are capable of forming plans and executing on them in just the way that human beings can. An AGI could learn not only to play board games but also to drive, to have conversations, to do mathematics, and countless other tasks. So far, artificial intelligence has been narrow. AlphaGo is extraordinarily good at playing Go but is incapable of doing anything else.41 But some of the leading AI labs, such as DeepMind and OpenAI, have the explicit goal of building AGI.42 And there have been indications of progress, such as the performance of GPT-3, an AI language model which can perform a variety of tasks it was never explicitly trained to perform, such as translation or arithmetic.43 AlphaZero, a successor to AlphaGo, taught itself how to play not only Go but also chess and shogi, ultimately achieving world-class performance.44 About two years later, MuZero achieved the same feat despite initially not even knowing the rules of the game.45 The development of AGI would be of monumental longterm importance for two reasons. First, it might greatly speed up the rate of technological progress, economic growth, or both. These arguments date back over sixty years, to early computer science pioneer I. J. Good, who worked in Bletchley Park to break the German Enigma code during World War II, alongside Alan Turing and, as it happens, my grandmother, Daphne Crouch.46 Recently, the idea has been analysed by mainstream growth economists, including Nobel laureate William Nordhaus.47 There are two ways in which AGI could accelerate growth. First, a country could grow the size of its economy indefinitely simply by producing more AI workers; the country’s growth rate would then rise to the very fast rate at which we can build more AIs.48 Analysing this scenario, Nordhaus found that, if the AI workers also improve in productivity over time because of continuing technological progress, then growth will accelerate without bound until we run into physical limits.49 The second consideration is that, via AGI, we could automate the process of technological innovation. We have already seen this recently to some extent: DeepMind’s machine-learning system AlphaFold 2 made a huge leap towards solving the “protein folding problem”—that is, how to predict what shape a protein will take—reaching a level of performance that had been regarded as decades away.50 If AGI could quite generally automate the process of innovation, the rate of technological progress we have seen to date would greatly increase. This acceleration would apply to the design of AI systems themselves, in a positive feedback loop. This idea was formalised in a model by some leading growth economists; again, they found that AI could produce extraordinarily fast—and accelerating—rates of growth.51 It’s not inevitable that AI will impact technological progress in this way. Indeed, the authors of the models I’ve referenced emphasise that accelerating growth rates hold only under some conditions.52 Perhaps, for example, there are some crucial inputs that are very hard to automate; perhaps these include the manufacturing of computer chips, or the mining of ores to create those chips, or the building of power plants to power the server farms the AI systems rely on. If so, then the slow growth in these areas would constrain the overall rate of progress. However, given the clear mechanisms by which AI could generate far faster growth rates, we should take this possibility very seriously. Economies could double in size over months or years rather than decades. This might seem implausible, but, remarkably, moving to much faster rates of economic growth would be a continuation of historical trends. We are used to thinking about growth in terms of a steady exponential, where a country’s economy grows by a few percent every year. But over the long run growth rates have accelerated. In the early agricultural era, the global rate of economic growth was around 0.1 percent per year; nowadays, it is around 3 percent per year.53 Before the Industrial Revolution, it took many centuries for the world economy to double in size; now it doubles every twenty-five years. It’s not clear how best to understand this. Perhaps history was a succession of distinct exponential “growth modes”—moving from a hunter-gather economic era to an agricultural era to an industrial era.54 Or perhaps economic history is just a single faster-than-exponential but noisy trend, with rates of growth steadily accelerating over time. In this latter view, the last one hundred years of relatively stable growth rates are anomalously slow.55 But in either the “growth modes” view or the “single faster-than-exponential trend” view, we should be open to the idea that growth rates might be much higher in the future than they are today. Given that growth rates have increased thirtyfold since the agricultural era, it’s not crazy to think that they might increase tenfold again; but if they did, the world economy would double every two and a half years.56 An increase in the rate of technological progress is the first reason why AGI would be a monumental event. The second reason, crucial from a longterm perspective, is AGI’s potential longevity.57 In Chapter 1 we saw that Shakespeare and Horace really might have achieved immortality through their poetry. Information can persist indefinitely because the cost to replicate it is so tiny. But software is just complex information. It can be replicated easily. For example, one of the first commercially available computer games was Pong by Atari, released in 1977.58 You can still play it today online.59 Though eventually all original Atari consoles will rust and crumble, Pong will live on. The software that defines Pong is replicable, and if every future generation is willing to pay the tiny cost of replicating this little piece of history, it will continue to persist. Pong could last as long as civilisation does. There’s nothing different in principle between the software that encodes Pong and the software that encodes an AGI. Since that software can be copied with high fidelity, an AGI can survive changes in the hardware instantiating it. AGI agents are potentially immortal.
```

#### Output text by wordtune (ideal size: 312 words, 1969 characters | actual size: 287 words, 1875 characters)
```
    Machine learning has made enormous progress in artificial intelligence over the last ten years, with breakthroughs in generating and recognising speech, images, art, and music, in real-time strategy games like StarCraft, and in a wide variety of tasks associated with understanding and generating humanlike text. AI research aims to create artificial general intelligence (AGI), which would be capable of learning and performing tasks in the same way that humans do.

    Artificial intelligence has been narrow, and AlphaGo is extraordinarily good at playing Go but incapable of doing anything else. However, there have been indications of progress towards building artificial general intelligence, which could greatly accelerate technological progress and economic growth.

    The arguments for artificial general intelligence (AGI) date back over sixty years, and include I. J. Good's idea that a country could grow its economy indefinitely by producing more AI workers, and William Nordhaus's idea that AGI could automate the process of technological innovation.

    Some leading growth economists believe that AI could produce extraordinarily fast - and accelerating - rates of growth. If certain crucial inputs are hard to automate, then this could constrain the overall rate of progress.

    Economies could double in size over months or years rather than decades. The trend of faster growth over the long run is a continuation of historical trends, and the last one hundred years of relatively stable growth rates are anomalously slow.

    If growth rates increase tenfold again, the world economy would double every two and a half years, which is the first reason why AGI would be a monumental event.

    Information can persist indefinitely because the cost to replicate it is so tiny. Software can be replicated easily, and Pong can last as long as civilisation does.
```

## AssemblyAI
![image](https://user-images.githubusercontent.com/910321/206804723-7b6ee5c6-5813-4eaf-87d7-3b60e08458fd.png)

Input text: 2nd chapter of AI Superpowers, 7465 words, 45855 characters

Output summaries
* gist: 324 words, 2104 characters, x21 ratio
* paragraph: 324 words, 2104 characters, x21 ratio
