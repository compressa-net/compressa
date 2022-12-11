# Compressa.NET
## Books, podcasts summarized for busy people

Compressa.NET application was built in a way that it can be deployed to Windows, Linux, Mac, Android and iOS, but since I have only Windows at the moment, only the Windows release is guarantied to work!

![image](https://user-images.githubusercontent.com/910321/206875910-52a29f87-0eb1-4588-b906-c19809a84f47.png)

![image](https://user-images.githubusercontent.com/910321/206875918-1283b249-4873-4cb9-8554-b203778634cc.png)

![image](https://user-images.githubusercontent.com/910321/206875927-df332a8f-bb3c-4b09-839e-f817056439af.png)

![image](https://user-images.githubusercontent.com/910321/206906709-ce2cf9e8-4e61-4d8b-a399-19783ca1ea7d.png)


### Audio processing by 

![image](https://user-images.githubusercontent.com/910321/206879373-d3dc58fd-2584-46b3-8a20-468a08e409c8.png)

### Audio transcription and text summarization by

![image](https://user-images.githubusercontent.com/910321/206879590-90a4d3a8-2511-42f6-96da-c536c5d37b48.png)

### Text sentiment detection by

![image](https://user-images.githubusercontent.com/910321/206879433-d89bd3aa-bbf3-466c-bdc7-8543f3411e99.png)

## Application Installation
This application was created with Microsoft latest, platform independent, open-source framework called .NET MAUI.

The generated application packages can be downloaded from [the Releases page](https://github.com/compressa-net/compressa/releases).

A video showing you the basic functionality [can be watched on YouTube](https://youtu.be/q9bRZmZh9nY).

## Future Potencial

I love technologies that make us more efficient or make our lifes easier. This app could help countless people who love to read, but have little time for it. It could mean a more efficient way to consume information from books.

Integrating AI-generated voices would be a logical next step, and automatic translation should be fairly easy to implement too. I could image a business model where we contact authors diretly and share our revenue with them, based on their statistics in our app.

It would be a win-win: fantastic way for us to consume these important works, and we would give the authors a new revenue channel to profit from.

## AI services that were explored
&cross; Stable Diffusion - logo

&check; AssemblyAI - audio to text, chapter summary

&cross; co:here - text compression (!! generated low quality summaries, I can't use them.)

&cross; wordtune - text compression (!! they don't have a public API, I can't use them.)

&check; ChatGPT - text compression (works very good, but no public API available yet)

&cross; Uberduck - text-to-speech (sounds good, integration should be straightforward)

&cross; Microsoft Speech Studio - customized text-to-speech (!! seems like a good option for custom voices, but needs special permission)

&cross; Google Translate - translation

&cross; co:here - fine-tuned chat-bot for every book (!! I'm not sure it's possible)

&check; co:here - use their fine-tuned model for sentiment analyzis

&check; GitHub Copilot - programming

## Steps to completion
- [x] Start a new solution the basic templates of .NET 7 ASP.NET and MAUI.
- [x] Use FFMPEG to convert audible audiobooks to m4b and mp3 chapter files
- [x] Use the AssemblyAI service to transcribe the chapters, and test their summarization method
- [x] Use ChatGPT to generate x4 and x16 compressed text
- [x] Create a basic GUI
- [x] List available audiobooks
- [x] Show the summaries for at least one book
- [x] Make the UI capable to change zoom levels amoung 4 levels (list of books, chapters + x64 summary, x16 summary and x4 summary)
- [x] Setup the domain and publish the app to [app.compressa.net](https://app.compressa.net)
- [x] (optional) Use co:here to build a book-centric chatbot
- [ ] (optional) Generate AI-voices for the summaries (skipped)
- [ ] (optional) Use Google Translate API to translate the summaries to Spanish (skipped)
- [ ] (optional) Publish the application on the Microsoft Store (failed)
- [x] Technical documentation
- [x] Make a-2 minute video
- [ ] Submit the project on Devpost before 21:00

## Technical details

### Project structure
![image](https://user-images.githubusercontent.com/910321/206907696-67724d50-88fb-4818-abf5-a79b4786c64f.png)

There are three project in the Visual Studio solution: one for the backend, one for the frontend, one library for the shared classes.

#### Backend web API - [Compressa.API](https://github.com/compressa-net/compressa/tree/main/src/Compressa/Compressa.API)

The backend has multiple responsibilities:
 - Convert the Audible Audiobook format (AAX) into an open standard (M4B)
 - Extract the chapter information from the M4B into JSON
 - Split the M4B file into separate MP3 files, one file per chapter
 - Transcribe the MP3s one by one
 - Summarize the text from the chapter on 3 different levels
 - Analyze those text and guess their sentiment
 - Save and update all the metadata associated with the audiobooks
 - Prepare the data for the front-end consumption
 - Expose many of the above functionalities as a standard RESTful Web API

To accomplish all of these tasks I used FFMPEG, AssemblyAI, co:here and OpenGPT. FFMPEG has a C# library, but I wrote my own library to the others. Nearly all the step of this pipeline is now automated and accessible through the API interface.

Here is a screenshot of its Swagger interface (that can be accessed at https://localhost:7283/swagger/index.html while running it locally):

![image](https://user-images.githubusercontent.com/910321/206909030-94a7d433-8fe1-417d-834e-c41e1f3814ef.png)

The backend also has a console output logger, where we can follow the different (often time-consuming) tasks:
![image](https://user-images.githubusercontent.com/910321/206909255-e0a62241-6b95-43bd-86ba-e995e6f2a8b5.png)

You can call these functions easily by openning their URLs.

https://localhost:7283/compressa/transcribechapter/AISuperpowers/08

Looks for the chapter 8 MP3 of the book "AI Superpowers" and automatically uses AssemblyAI's API to transcribe that chapter. If it was successful it updates the metadata files.

https://localhost:7283/compressa/getsentiment/AISuperpowers/04

Similarly this one tries to open the files associated with AI Superpower's chapter 4, and run co:here's sentiment analizer.

https://localhost:7283/compressa/getallmetadata

This is probanly the easiest to test, because it returns all the metadata we have on the backend. This returned JSON file can be cached on the frontend for offline usage.

#### .NET MAUI frontend [Compressa.GUI](https://github.com/compressa-net/compressa/tree/main/src/Compressa/Compressa.GUI)

The frontend was based on a public MAUI template, [Microsoft's Point of Sale sample](https://docs.microsoft.com/de-de/samples/dotnet/maui-samples/apps-pointofsale/).

My priority was to have the simplest design possible that is still functional. The frontend first consumed the backend's API, but then I realized that deploying a backend, having a fixed URL for it with SSL certificates is too much time and I decided to have a trick: I used caching when I called the backend, and I saved the cache. So now the frontend is able to retrieve the data from its cache and run without the presence of the backend.

![image](https://user-images.githubusercontent.com/910321/206909620-b5922142-cd1f-4233-9664-3770b02c1329.png)

### How to compile it and run it

The most robust way to test it if you compile it from source. To do that you need the following steps:
1. Install Visual Studio 2022 (the free Community Edition is fine)
2. Make sure that you install the `ASP.NET and web development` and the `.NET MAUI Multi-platform App UI development` workloads
3. Clone this repo
4. Open the `Compressa.sln` file from the `\src\Compressa` folder
5. Select the `Compressa.GUI` project from the `Solution Explorer` on the right side, right click and `Set as Startup Project`
6. Run the solution with the little green play button on the top bar (or press F5)

IMPORTANT: This code is not production ready and it has my API keys for AssemblyAI and co:here! Feel free to use them for testing, just make sure that you don't share it with others.

## My Schedule

### Friday (Backend day)

### Saturaday (Frontend day)

### Sunday (Publication and Presentation day)

(note: all of my work was screen captured and available on request)

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

#### Output text (ideal size: 312 words, 1969 characters | actual size: 287 words, 1875 characters)
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

### Input text (7465 words, 45855 characters)
2nd chapter of AI Superpowers

### Output texts
* `gist`: 11 words, 73 characters, x630 ratio
* `paragraph`: 56 words, 362 characters, x126 ratio
* `bullets`: 324 words, 2104 characters, x21 ratio
* `verbose bullets`: 327 words, 2104 characters, x21 ratio

### Conclusion
- `gist` would be great for chapter titles
- `paragraph` will be shown under the chapter header
- `verbose bullets` will be the first level of zoom

## ChatGPT

The chatbot could generate the summary with lowest zoom level. The target compression would be x4.

### Input text (340 sentences, 7465 words, 45855 characters)
2nd chapter of AI Superpowers, diveded into ~50-sentence parts.

`Summarize the following text with approximately 15 sentences:`

#### Segment-1 (5592 characters to 909 characters, x6.15 ratio)
```
Summarize the following text with approximately 13 sentences: One China's sputnik moment, the Chinese teenager with the square rimmed glasses seemed an unlikely hero to make humanity's last stand. Dressed in a black suit, white shirt and black tie, ku Ja slumped in his seat, rubbing his temples and puzzling over the problem in front of him. Normally filled with a confidence that bordered on cockpit, the 19 year old squirmed in his leather chair changed the venue. And he could be just another prep school kid agonizing over an insurmountable geometry proof. But on this May afternoon in 2017, he was locked in an all out struggle against one of the world's most intelligent machines, AlphaGo, a powerhouse of artificial intelligence backed by arguably the world's top technology company, Google. The battlefield was a 19 by 19 lined board populated by little black and white stones, the raw materials of the deceptively complex game of Go. During gameplay, two players alternate placing stones on the board, attempting to encircle the opponent's stones. No human on Earth could do this better than Khaja. But today he was pitted against a Go player on a level that no one had ever seen before. Believed to have been invented more than 2500 years ago, goh's history extends further into the past than any board game still played today. In ancient China, Go represented one of the four art forms any Chinese scholar was expected to master. The game was believed to imbue its players with a Zenlike intellectual refinement and wisdom where games like Western chess were crudely tactical. The game of Go is based on a patient positioning and slow encirclement, which made it into an art form, a state of mind. The depth of Go's history is matched by the complexity of the game itself. The basic rules of gameplay can be laid out in just nine sentences, but the number of possible positions on a Go board exceeds the number of atoms in the known universe. The complexity of the decision tree had turned, defeating the world champion of Go into a kind of Mount Everest for the artificial intelligence community, a problem whose sheer size had rebuffed every attempt to conquer it. The poetically inclined said it couldn't be done because machines lacked the human element, an almost mystical feel for the game. The engineers simply thought the board offered too many possibilities for a computer to evaluate. But on this day, AlphaGo wasn't just beating Kajia. It was systematically dismantling him. Over the course of three marathon matches of more than 3 hours each, kerr had thrown everything he had at the computer program. He tested it with different approaches conservative, aggressive, defensive and unpredictable. Nothing seemed to work. AlphaGo gave Kerr no openings. Instead, it slowly tightened its vise around him. The view from Beijing what you saw in this match depended on where you watched it from. To some observers in the United States, AlphaGo's victories signaled not just the triumph of machine over man, but also of Western technology companies over the rest of the world. The previous two decades had seen Silicon Valley companies conquer world technology markets. Companies like Facebook and Google had become the goto Internet platforms for socializing and searching. In the process, they had steamrolled local startups in countries from France to Indonesia. These Internet juggernauts had given the United States a dominance of the digital world that matched its military and economic power in the real world. With AlphaGo, a product of the British AI startup Deep Mind, which had been acquired by Google in 2014, the west appeared poised to continue that dominance into the age of artificial intelligence. But looking out my office window during the Kajia match, I saw something far different. The headquarters of my venture capital fund is located in Beijing's Jong Guan Soon neighborhood, an area often referred to as the Silicon Valley of China. Today, Zhang Guangsoon is the beating heart of China's AI movement. To people here, AlphaGo's victories were both a challenge and an inspiration. They turned into China's Sputnik moment for artificial intelligence. When the Soviet Union launched the first human made satellite into orbit in October 1957, it had an instant and profound effect on the American psyche and government policy. The event sparked widespread us. Public anxiety about perceived Soviet technological superiority, with Americans following the satellite across the night sky and tuning into Sputnik's radio transmissions. It triggered the creation of the National Aeronautics and Space Administration. NASA fueled major government subsidies for math and science education, and effectively launched the space race that nationwide American mobilization bore fruit. Twelve years later, when Neil Armstrong became the first person ever to set foot on the moon, AlphaGo scored its first high profile victory in March 2016, during a five game series against the legendary Korean player Lee Zaidal, winning four to one. While barely noticed by most Americans, the five games drew more than 280,000,000 Chinese viewers. Overnight, China plunged into an artificial intelligence fever. The buzz didn't quite rival America's reaction to Sputnik, but it lit a fire under the Chinese technology community that has been burning ever since. When Chinese investors, entrepreneurs, and government officials all focus in on one industry, they can truly shake the world. Indeed, China is ramping up AI investment, research, and entrepreneurship on a historic scale.
```

```
In May 2017, Ku Ja, a 19-year-old Chinese teenager and Go world champion, lost to AlphaGo, a powerful artificial intelligence (AI) program developed by Google. The game of Go is considered to be one of the most complex board games in the world. The number of possible positions on a Go board is greater than the number of atoms in the known universe, making it a significant challenge for AI. The victory of AlphaGo, a Western technology, over the human world champion was seen by some as a victory for Western technology companies over the rest of the world. In Beijing, however, the view was different. The victories were seen as a challenge for Chinese technology companies to catch up with the West in AI. This sentiment was echoed by the Chinese government, which made a push to become a global leader in AI. By 2022, China is expected to surpass the US as the world's largest market for AI applications.
```

#### Segment-2 (5821 characters to 821 characters, x7.09 ratio)
```
Summarize the following text with approximately 13 sentences: Money for AI startups is pouring in from venture capitalists, tech juggernauts, and the Chinese government. Chinese students have caught AI fever as well, enrolling in advanced degree programs and streaming lectures from international researchers on their smartphones. Startup founders are furiously pivoting, reengineering or simply rebranding their companies to catch the AI wave. And less than two months after Kojia resigned his last game to AlphaGo, the Chinese central government issued an ambitious plan to build artificial intelligence capabilities. It called for greater funding, policy support, and national coordination for AI development. It set clear benchmarks for progress by 2000 and 22,025, and it projected that by 2030, China would become the center of global innovation in artificial intelligence. Leading in theory, technology and application. By 2017, Chinese venture capital investors had already responded to that call, pouring record sums into artificial intelligence startups and making up 48% of all AI venture funding globally, surpassing the United States for the first time. A game and a Game Changer underlying that surge in Chinese government support is a new paradigm in the relationship between artificial intelligence and the economy. While the science of artificial intelligence made slow but steady progress for decades, only recently did progress rapidly accelerate, allowing these academic achievements to be translated into real world use cases. The technical challenges of beating a human at the game of Go were already familiar to me. As a young PhD student researching artificial intelligence at Carnegie Mellon University, I studied under Pioneer AI researcher Raj Reddy. In 186, I created the first software program to defeat a member of the world championship team for the game of a Fellow. A simplified version of Go played on an eight by eight square board. It was quite an accomplishment at the time, but the technology behind it wasn't ready to tackle anything but straightforward board games. The same held true when IBM's Deep Blue defeated world chess champion Gary Kasparov in a 90 97 match. Dubbed the Brain's Last Stand, the event had spawned anxiety about when our robot overlords would launch their conquest of humankind. But other than boosting IBM's stock price, the match had no meaningful impact on life in the real world. Artificial intelligence still had few practical applications, and researchers had gone decades without making a truly fundamental breakthrough. Deep Blue had essentially brute forced its way to victory, relying largely on hardware customized to rapidly generate and evaluate positions from each move. It had also required real life chess champions to add guiding heuristics to the software. Yes, the win was an impressive feat of engineering, but it was based on long established technology that worked only on very constrained sets of issues. Remove Deep Blue from the geometric simplicity of an eight by eight square chessboard, and it wouldn't seem very intelligent at all. In the end, the only job it was threatening to take was that of the world chess champion. This time, things are different. The Kurgea versus AlphaGo match was played within the constraints of a Go board, but it is intimately tied up with the dramatic changes in the real world. Those changes include the Chinese AI frenzy that AlphaGo's matches sparked amid the underlying technology that powered it to victory. AlphaGo runs on deep learning, a groundbreaking approach to artificial intelligence that is turbocharged the cognitive capabilities of machines. Deep learning based programs can now do a better job than humans at identifying faces, recognizing speech and issuing loans. For decades, the artificial intelligence revolution always looked to be five years away. But with the development of deep learning over the past few years, that revolution has finally arrived. It will usher in an era of massive productivity increases, but also widespread disruptions in labor markets and profound sociopsychological effects on people as artificial intelligence takes over human jobs across all sorts of industries. During the Kojia match, it wasn't the aid driven killer robots some prominent technologists sworn of that frightened me. It was the real world demons that could be conjured up by mass unemployment and the resulting social turmoil. The threat to jobs is coming far faster than most experts anticipated, and it will not discriminate by the color of one's caller, instead striking the highly trained and poorly educated alike. On the day of that remarkable match between AlphaGo and Kaja, the deep learning was dethroning. Humankind's best go player. That same job eating technology is coming soon to a factory and an office near you, the ghost in the Go machine. But in that same match, I also saw a reason for hope. 2 hours and 51 minutes into the match, Kudja had hit a wall. He'd given all that he could to this game, but he knew it wasn't going to be enough. Punched low over the board, he pursed his lips and his eyebrows began to twitch. Realizing he couldn't hold his emotions in any longer, he removed his glasses and used the back of his hand to wipe tears from both of his eyes. It happened in a flash, but the emotion behind it was visible for all to see. Those tears triggered an outpouring of sympathy and support for Ku. Over the course of these three matches, Ku had gone on a roller coaster of human emotion confidence, anxiety, fear, hope and heartbreak. It had showcased his competitive spirit, but I saw in those games an act of genuine love. A willingness to tangle with an unbeatable opponent out of pure love for the game, its history and the people who play it.
```

```
The game of Go is considered to be one of the most complex board games in the world, with more possible positions than there are atoms in the known universe. In May 2017, Ku Ja, a Chinese teenager and Go world champion, lost to AlphaGo, a powerful artificial intelligence program developed by Google. The victory of a Western technology over the human world champion was seen as a victory for Western technology companies over the rest of the world. In Beijing, however, the view was different. The victories were seen as a challenge for Chinese technology companies to catch up with the West in artificial intelligence. This sentiment was echoed by the Chinese government, which made a push to become a global leader in AI. By 2022, China is expected to surpass the US as the world's largest market for AI applications.
```

## Challenges regarding the publishing

Publishing to the Microsoft Store isn't straightforward at all.

When we create a new MSIX file, the publishing profile (that is found in `Compressa.GUI\Properties\PublishProfiles`) must have its `SelfContained` value set to `true` and its `PublishAppxPackage` to `true`. The usable files including the MSIX file will be in the AppxPackageDir, and not in `PublishDir`!
